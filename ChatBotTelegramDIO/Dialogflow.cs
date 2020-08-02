﻿using Google.Cloud.Dialogflow.V2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace ChatBotTelegramDIO
{
    class Dialogflow
    {
        private SessionsClient _sessionsClient;
        private SessionName _sessionName;
        Root chatbottelegramdio = JsonConvert.DeserializeObject<Root>(@"{
  ""type"": ""service_account"",
  ""project_id"": ""chatbottelegramdio-xhsn"",
  ""private_key_id"": ""6093aebf3dcf51aa05d3c1c4bc8288d7863b082c"",
  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC0BkO4I8GTyTWD\nwh0xZrwqhi0j4DAH+YVu4gK4n09uk/Yls9EE/CHMW1zZnDdonWhG0DMLa+BNJZmH\nhz/H1s+De5T64pXB5RH4zKQMS26P8kTxbe3DBRB4Lgx/HV1nxNeTbrBT8PRq2GlW\ngAF0KHVn6I7fCF0NO3z2n7WJaWCX0ZnDaP4g43gRoEyWKmi7R57fKVWcVkC4cvUF\nHRvizhhZhmMGOCD9vrjt0zpJ1E774+kJN0yrwIZvWyC73H8gG6qXA1aHiO1Cn2pO\nxTGHL5gxrSRJBAthKk4Zfp6zRKc0z4mwehGMH1/vIojXqfZfGHEZvrbhy0XUVRnY\nYqt4Zrj1AgMBAAECggEAAIeUMDjkTn7+o7VkFWUfLWQMdfKO5NC2Ti4A1aVovANY\nOG9fygsqPkoQHkPE/FwzyYfxq0yIWGT0uRIllrAdfcS7Td5xk/E/xq+Z1Yw5qXEl\n0SzPNw4PKL6NItwydIx+5dwjGkKEAlx1bHlSVJLATBH1eaQYqGIbnme4RpmcwyJf\nC9kCVfm1efmRpLqx2JkG1sJ3n+DvMt6TleQGBZAEO+oNT0q5e6H9G1TkF6l2R1xB\nKs1wJus3jBgqtqMwNBOi6P3PlyCAmI2NBmbXm8MrTYtWrNwh6aU1tO/1sbLdmoFL\ngh81srF2P7dd9ypz3YMCdboIMWU54K0atqALwvFaAQKBgQD2IKQqFkPxLr2BIrAW\nx01yt3cQ6NK3xFU3k/j8Tcw+nsZV3usiCZEBAxj2bMqRXh4m0PUI7XO20FWPcZho\nF3BZz3LLN3SOQbx150YSBd5uYPt3bi+B4SDy0O7uR+s86fUn8Prv2bNTwsZYm1Qv\ndsRv1EgSi/5ilRxmsoSb0JTm1QKBgQC7Ptg8bVollHxDnpSkr121d8EpJ55A+1G3\ngqf1HGRgudbbkXq0p1Yp8rKILMQ+HiBpLm0Uh+a/iigUeJqbukavjC8bmDBbelF0\n/5LC0YvD9H1VfM/LH3nAiDUXYQu8Y69X5mCQ1HeLI9dMvV+Pt+WR0hadqOU44xiU\ns7sGYDvZoQKBgAPL/xxTGZs34F6EnXjMfEpfwCt3nACPu8zOsJGb1aHFq0OZ28C7\nqwv78z+h8AIMFKT+pjkMCLPyRM+grfw1GFuaUqgF4/tp0jFzbuZKRuMnBdoSzhLP\n2UVFqdntLBjVdx21nvliP1z0bUDirIrK5z7eZHo8xKDasgR3jookzpc9AoGBAKxR\nMX6qxANBmvo/vluwZ3TROJo/M4xpvNI9E4SnFFlPrUNzppEKTmlcSKC5UgA4iWtC\npm/2gYxUAH6WXvJhgxuazt9+N0J4Vu5tJQrU/OLg5Vb+/dvYo5tjvjg9vycoNf2W\nrdebMEiEO2cMAB2rB0IjLVz6SbkIhV6T6NH9ThcBAoGAELG8/6VSLJGywPrS3AFh\nigsetqGJjadidBfDFCixKUCCZalVk0tLn8GjuMNjXztkvWxdcnGcbj1n2OUoFz7z\nsjsz+71y+6vQcvabzfNbp/Rj52MdmBG6f9vFG3V194VnJFfSssceN8Fo5HkZ4Wjn\ncC9DDJYmOH2NsqqQifBsJmQ=\n-----END PRIVATE KEY-----\n"",
  ""client_email"": ""dialogflow-tbsdli@chatbottelegramdio-xhsn.iam.gserviceaccount.com"",
  ""client_id"": ""103736899082417307840"",
  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
  ""token_uri"": ""https://oauth2.googleapis.com/token"",
  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/dialogflow-tbsdli%40chatbottelegramdio-xhsn.iam.gserviceaccount.com""
}");

        public async Task<QueryResult> VerificarIntent(string chatID, string message)
        {
            _sessionsClient = await SessionsClient.CreateAsync();
            _sessionName = new SessionName(chatbottelegramdio.project_id, chatID);

            QueryInput queryInput = new QueryInput();
            var queryText = new TextInput();
            queryText.Text = message;
            queryText.LanguageCode = "pt-BR";
            queryInput.Text = queryText;

            DetectIntentResponse response = await _sessionsClient.DetectIntentAsync(_sessionName, queryInput);
            return response.QueryResult;
        }
    }
}
