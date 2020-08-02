using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Google.Cloud.Dialogflow.V2;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Newtonsoft.Json;
using Google.Api.Gax;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotTelegramDIO
{
    class Program
    {
        static TelegramBotClient Bot = new TelegramBotClient("1370584167:AAGHMBkGdPkYpNj9eL5w1Zb47Eq6E4w3D_8");
        static Dialogflow Dialogflow = new Dialogflow();
        static List<double> Valores = new List<double>();

        static void Main(string[] args)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"{caminho do arquivo .json}");            
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnMessage;

            Console.ReadLine();      

        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var chatID = e.Message.Chat.Id.ToString();
            var dialogflowQueryResult = Dialogflow.VerificarIntent(chatID, e.Message.Text);
            var resposta = "";        

            if (dialogflowQueryResult.Result.Intent.DisplayName.Contains("Welcome"))
            {
                resposta = "Olá, " + e.Message.From.FirstName + "! " + dialogflowQueryResult.Result.FulfillmentText;
            }
            else if (dialogflowQueryResult.Result.Intent.DisplayName == "Finalizar")
            {
                resposta = "Total: R$" + Valores.Sum().ToString("0.00") + ". " + dialogflowQueryResult.Result.FulfillmentText;
            }
            else if (dialogflowQueryResult.Result.Intent.DisplayName == "Reiniciar")
            {
                Valores.Clear();
                resposta = dialogflowQueryResult.Result.FulfillmentText;
            }
            else if (dialogflowQueryResult.Result.Intent.DisplayName == "Remover")
            {
                Valores.RemoveAt(Valores.Count - 1);
                resposta = dialogflowQueryResult.Result.FulfillmentText;
            }
            else
            {
                BuscarSiteCarrefour buscarSite = new BuscarSiteCarrefour(e.Message.Text);
                Valores.Add(double.Parse(buscarSite.preco));
                resposta = buscarSite.nome + " - Preço: R$" + buscarSite.preco + " - Total: R$" + Valores.Sum().ToString("0.00") + ". Link para este produto: " + buscarSite.link;
            }

            Bot.SendTextMessageAsync(chatID, resposta);
        }
    }
}
