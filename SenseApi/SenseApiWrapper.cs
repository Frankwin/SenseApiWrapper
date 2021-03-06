﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SenseApi.Enums;
using SenseApi.Models;

namespace SenseApi
{
    public class SenseApiWrapper
    {
        public AuthorizationResponse AuthorizationResponse { get; set; }
        public MonitorStatus MonitorStatus { get; set; }
        public List<Device> DeviceList { get; set; }
        public static IConfigurationRoot Config { get; private set; }
        private readonly string apiAddress;

        public SenseApiWrapper()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            apiAddress = Config["api-url"];
        }
        
        /// <summary>
        /// Authenticate with the Sense API using your email and password
        /// </summary>
        /// <param name="email">Email to use to authenticate</param>
        /// <param name="password">Password to use to authenticate</param>
        /// <returns>Authorization Response object</returns>
        /// <exception cref="HttpRequestException">Exception thrown if the request was not successfull.</exception>
        public async Task<AuthorizationResponse> Authenticate(string email, string password)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password)
            };

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = await httpClient.PostAsync($"{apiAddress}/authenticate", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new HttpRequestException("The provided username and/or password are incorrect.");
                        }

                        throw new HttpRequestException($"{(int)response.StatusCode} - {response.ReasonPhrase}");
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    AuthorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(json);

                    // Update AccessToken and Monitor ID in appsettings.json file
                    var appSettingsJson = File.ReadAllText("appsettings.json");
                    dynamic jsonObj = JsonConvert.DeserializeObject(appSettingsJson);
                    jsonObj["accesstoken"] = AuthorizationResponse.AccessToken;
                    jsonObj["monitor-ids"] = string.Join(",", AuthorizationResponse.Monitors.Select(x => x.Id));
                    string output =
                        JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    File.WriteAllText("appsettings.json", output);

                    Config.Reload();

                    return AuthorizationResponse;
                }
            }
        }

        /// <summary>
        /// Get the status of the Sense Monitor from the Sense API
        /// </summary>
        /// <param name="monitorId">Monitor ID to check</param>
        /// <returns>Monitor Status Object</returns>
        public async Task<MonitorStatus> GetMonitorStatus(int monitorId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/monitors/{monitorId}/status";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                MonitorStatus = JsonConvert.DeserializeObject<MonitorStatus>(json);

                return MonitorStatus;
            }
        }

        /// <summary>
        /// Get the list of devices that Sense "detected" from the Sense API 
        /// </summary>
        /// <param name="monitorId">Monitor ID to get the devices from</param>
        /// <returns>List of Devices with some details</returns>
        public async Task<List<Device>> GetDeviceList(int monitorId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/monitors/{monitorId}/devices";
                
                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                DeviceList = JsonConvert.DeserializeObject<List<Device>>(json);

                return DeviceList;
            }
        }

        /// <summary>
        /// Get additional details on a specific device.
        /// If a DeviceList is present this list is also updated with the additional details.
        /// </summary>
        /// <param name="monitorId">Monitor ID that the device is registered on</param>
        /// <param name="deviceId">Device ID to get additional details for</param>
        /// <returns>Device Details for the specified device.</returns>
        public async Task<DeviceDetails> GetDeviceDetails(int monitorId, string deviceId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/monitors/{monitorId}/devices/{deviceId}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var deviceDetails = JsonConvert.DeserializeObject<DeviceDetails>(json);

                if (DeviceList == null || DeviceList.Count <= 0) return deviceDetails;

                var device = DeviceList.FirstOrDefault(x => x.Id == deviceId);
                if (device == null) return deviceDetails;

                var pos = DeviceList.FindIndex(x => x.Id == deviceId);
                DeviceList.RemoveAt(pos);
                device = deviceDetails.Device;
                DeviceList.Insert(pos, device);

                return deviceDetails;
            }
        }

        /// <summary>
        /// Get additional details for the "Always On" devices
        /// If a DeviceList is present this list is also updated with the additional details.
        /// </summary>
        /// <param name="monitorId">Monitor ID that the device is registered on</param>
        /// <returns>Device Details for the specified device.</returns>
        public async Task<DeviceDetails> GetAlwaysOnDetails(int monitorId)
        {
            const string deviceId = "always_on";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/monitors/{monitorId}/devices/{deviceId}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var deviceDetails = JsonConvert.DeserializeObject<DeviceDetails>(json);

                if (DeviceList == null || DeviceList.Count <= 0) return deviceDetails;

                var device = DeviceList.FirstOrDefault(x => x.Id == deviceId);
                if (device == null) return deviceDetails;

                var pos = DeviceList.FindIndex(x => x.Id == deviceId);
                DeviceList.RemoveAt(pos);
                device = deviceDetails.Device;
                DeviceList.Insert(pos, device);

                return deviceDetails;
            }
        }

        /// <summary>
        /// Get additional details for the "Always On" devices
        /// If a DeviceList is present this list is also updated with the additional details.
        /// </summary>
        /// <param name="monitorId">Monitor ID that the device is registered on</param>
        /// <returns>Device Details for the specified device.</returns>
        public async Task<DeviceDetails> GetOtherDetails(int monitorId)
        {
            const string deviceId = "unknown";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/monitors/{monitorId}/devices/{deviceId}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var deviceDetails = JsonConvert.DeserializeObject<DeviceDetails>(json);

                if (DeviceList == null || DeviceList.Count <= 0) return deviceDetails;

                var device = DeviceList.FirstOrDefault(x => x.Id == deviceId);
                if (device == null) return deviceDetails;

                var pos = DeviceList.FindIndex(x => x.Id == deviceId);
                DeviceList.RemoveAt(pos);
                device = deviceDetails.Device;
                DeviceList.Insert(pos, device);

                return deviceDetails;
            }
        }

        /// <summary>
        /// Get the generic monitor history samples from the Sense monitor
        /// </summary>
        /// <param name="monitorId">Monitor ID to get the history sampling from</param>
        /// <param name="granularity">Granularity to use, valid uses are Second, Minute, Hour and Day</param>
        /// <param name="startDateTime">Start Date and Time to start the sample retrieval from</param>
        /// <param name="sampleCount">Number of samples to retrieve.</param>
        /// <returns>HistoryRecord object with a list sampling values for the monitor in the chosen granularity interval</returns>
        public async Task<HistoryRecord> GetMonitorHistory(int monitorId, Granularity granularity, DateTime startDateTime, int sampleCount)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/history/usage?monitor_id={monitorId}&granularity={granularity.ToString().ToLowerInvariant()}&start={startDateTime.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ",CultureInfo.InvariantCulture)}&frames={sampleCount}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var history = JsonConvert.DeserializeObject<HistoryRecord>(json);

                return history;
            }
        }

        /// <summary>
        /// Get the history samples for a specific device from the Sense monitor
        /// </summary>
        /// <param name="monitorId">Monitor ID to get the history sampling from</param>
        /// <param name="deviceId">Device ID to get the history sampling for</param>
        /// <param name="granularity">Granularity to use, valid uses are Second, Minute, Hour and Day</param>
        /// <param name="startDateTime">Start Date and Time to start the sample retrieval from</param>
        /// <param name="sampleCount">Number of samples to retrieve.</param>
        /// <returns>HistoryRecord object with a list sampling values for the monitor in the chosen granularity interval</returns>
        public async Task<HistoryRecord> GetDeviceHistory(int monitorId, string deviceId, Granularity granularity, DateTime startDateTime, int sampleCount)
        {
            if (granularity != Granularity.Second && granularity != Granularity.Minute && granularity != Granularity.Hour &&
                granularity != Granularity.Day)
            {
                throw new InvalidEnumArgumentException($"{granularity.ToString()} is not a valid granularity for this call.");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData =
                    $"{apiAddress}/app/history/usage?monitor_id={monitorId}&device_id={deviceId}&granularity={granularity.ToString().ToLowerInvariant()}&start={startDateTime.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture)}&frames={sampleCount}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var history = JsonConvert.DeserializeObject<HistoryRecord>(json);

                return history;
            }
        }

        /// <summary>
        /// Get Usage Trend Data from the Sense API
        /// </summary>
        /// <param name="monitorId">Monitor ID to get the trend data from</param>
        /// <param name="granularity">Granularity to use, valid uses are Hour, Day, Month and Year</param>
        /// <param name="startDateTime">Start Date and Time to start the trend data retrieval from</param>
        /// <returns>TrendData object</returns>
        public async Task<TrendData> GetUsageTrendData(int monitorId, Granularity granularity, DateTime startDateTime)
        {
            if (granularity != Granularity.Hour && granularity != Granularity.Day && granularity != Granularity.Month &&
                granularity != Granularity.Year)
            {
                throw new InvalidEnumArgumentException($"{granularity.ToString()} is not a valid granularity for this call.");
            }
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/app/history/trends?monitor_id={monitorId}&device_id=usage&scale={granularity.ToString().ToLowerInvariant()}&start={startDateTime.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture)}";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var trendData = JsonConvert.DeserializeObject<TrendData>(json);

                return trendData;
            }
        }

        /// <summary>
        /// Get the Timeline Data for a user
        /// </summary>
        /// <param name="userId">User ID to get the timeline data for</param>
        /// <returns>TimelineData object with the serialized json data</returns>
        public async Task<TimelineData> GetTimelineData(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config["accesstoken"]);

                var callData = $"{apiAddress}/users/{userId}/timeline";

                var response = await httpClient.GetAsync(callData);

                await CheckResponseStatus(response, httpClient, callData);

                var json = await response.Content.ReadAsStringAsync();

                var trendData = JsonConvert.DeserializeObject<TimelineData>(json);

                return trendData;
            }
        }

        #region Private methods

        /// <summary>
        /// Check the response status of the Http Call and if we get an Unauthorized response try
        /// to Authenticate again and then retry the call
        /// </summary>
        /// <param name="response">Response to check the status for</param>
        /// <param name="httpClient">HttpClient object to use to retry the call</param>
        /// <param name="callData">Call Data to retry</param>
        /// <exception cref="HttpRequestException">Exception thrown if the request was not successfull.</exception>
        private async Task CheckResponseStatus(HttpResponseMessage response, HttpClient httpClient, string callData)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode != HttpStatusCode.Unauthorized)
                    throw new HttpRequestException($"{(int)response.StatusCode} - {response.ReasonPhrase}");

                // Try to authenticate and retry the call if we got an Unauthorized Status Code as the token probably has expired
                await Authenticate(Config["email"], Config["password"]);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"{(int)response.StatusCode} - {response.ReasonPhrase}");
                }

                await httpClient.GetAsync(callData);

                throw new HttpRequestException($"{(int)response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        #endregion Private methods
    }
}
