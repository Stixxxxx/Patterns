using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using ConsoleApp1;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace TelegramBotExperiments
{
    class Program
    {

        static Utilits utilits = new Utilits();

        static long initedChat = -1;
        static long initedChatAdm = -1;
        static int count = 0;
        static Chat? initedChatClass;
        static Chat? initedChatAdmin;
        static Message message;
        static DateTime dateTimeNow;
        static DateTime dateTimeUpdate;
        static string anecdot = String.Empty;

        static ITelegramBotClient bot = new TelegramBotClient("6288696412:AAFG3wExbJ9mmwFSh0uI0CLITSuXrG-QacE");

        //пароли от анекдотов 24HMZHX4FB
        //                    wxn3V42e

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {


            string updateMessage = Newtonsoft.Json.JsonConvert.SerializeObject(update);

            // Вывод в консоль запроса к боту
            Console.WriteLine(updateMessage);

            //проверка даты
            if (update.Message == null) return;
            dateTimeUpdate = new DateTime(ticks: update.Message.Date.Ticks);
            double newHour = (double)3;
            dateTimeUpdate = dateTimeUpdate.AddHours(newHour);

            var buff = dateTimeNow.Ticks - dateTimeUpdate.Ticks + 3000000000;

            if ((dateTimeUpdate.Ticks + 600000000) <= dateTimeNow.Ticks) return;

            utilits.AddToLogFile(updateMessage, dateTimeUpdate);


            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                message = update.Message;
                if (message.Text != null && message.Chat.Title != null)
                {
                    if (message.Text.ToLower() == "/старт")
                    {

                        initedChat = message.Chat.Id;
                        initedChatClass = await botClient.GetChatAsync(initedChat);

                        //сделать проверку на разрешения!!!!!
                        await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);

                        //await botClient.SendTextMessageAsync(message.Chat, "Чат инициализирован. Сертоловский бот работает.");
                        return;
                    }
                }

            }

            // написать анонимно в чат
            //  Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                if (message.Text != null && initedChatClass != null)
                {
                    if (message.Text.ToLower().Contains("/анонимно") && message.Text.ToLower()[0] == '/')
                    {
                        await botClient.SendTextMessageAsync(initedChatClass, "Анонимное сообщение: " + message.Text.Remove(0, "/анонимно".Length));
                        return;
                    }
                }
                //await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
                //await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId); //удаляет сообщения
            }

            //работает бот сейчас?
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                message = update.Message;
                if (message.Text != null)
                {
                    if (message.Text.ToLower() == "/бот тут?")
                    {

                        await botClient.SendTextMessageAsync(message.Chat, "Бот работает.");
                        return;
                    }
                }
                // await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!" + count);
                //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения


            }


            //установка админа
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                message = update.Message;
                if (message.Text != null && message.Chat.Title == null)
                {
                    if (message.Text.ToLower() == "/7777")
                    {
                        initedChatAdm = message.Chat.Id;
                        initedChatAdmin = await botClient.GetChatAsync(initedChatAdm);


                        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[] {
                            new KeyboardButton[] { "/отправить в чат погоду" },
                            new KeyboardButton[] { "/запрос анекдота" },
                            new KeyboardButton[] { "/сохранить анекдот" },
                            new KeyboardButton[] { "/отправить в чат анекдот" },
                            new KeyboardButton[] { "/получить картинку наса" },
                            new KeyboardButton[] { "/пробки" },
                            new KeyboardButton[] { "/отправить пробки в чат" },
                            new KeyboardButton[] { "/запустить таймер" },

                        })
                        {
                            ResizeKeyboard = true
                        };
                        await botClient.SendTextMessageAsync(chatId: initedChatAdmin, text: "Кнопки установлены", replyMarkup: keyboard);
                        await botClient.SendTextMessageAsync(message.Chat, "Админ установлен.");

                        if (initedChatClass != null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Чат инициализирован. Сертоловский бот работает.");
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Необходимо инициализировать активный чат");

                        }



                        await botClient.SendTextMessageAsync(message.Chat, "Не забудьте проверить все разрешения для бота");


                        return;
                    }
                }
            }


            //писать от имени админа
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                message = update.Message;
                if (message.Text != null && initedChatAdmin != null && initedChatClass != null)
                {
                    if (message.Text.ToLower().Contains("/админ"))
                    {

                        await botClient.SendTextMessageAsync(initedChatClass, message.Text.Remove(0, "/ админ".Length));
                        return;

                    }
                    //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                }

                //получить погоду в Сертолово



                //запросить погоду
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null)
                    {
                        if (message.Text.ToLower() == "/погода")
                        {
                            string weather = utilits.GetStringFeatherSertolovo();

                            JObject json = JObject.Parse(weather);


                            var main = json["main"];
                            var b = json["weather"];
                            var bb = b[0];
                            var bbb = bb["description"];

                            var k = json["sys"];
                            var kk = k["country"];

                            var temp = main["temp"];


                            var c = json.GetValue("name");
                            var d = json.GetValue("name");

                            string result = "Сейчас погода в Сертолово." + "\nСостояние: " + bbb.ToString() + ".\nТемпература: " + temp.ToString() + " С°.";
                            await botClient.SendTextMessageAsync(message.Chat, result);
                            return;
                        }
                    }
                    // await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!" + count);
                    // await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения

                }


                //отправить в чат погоду
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                    {
                        if (message.Text.ToLower().Contains("/отправить в чат погоду"))
                        {

                            string weather = utilits.GetStringFeatherSertolovo();

                            JObject json = JObject.Parse(weather);


                            var main = json["main"];
                            var b = json["weather"];
                            var bb = b[0];
                            var bbb = bb["description"];

                            var k = json["sys"];
                            var kk = k["country"];

                            var temp = main["temp"];


                            var c = json.GetValue("name");
                            var d = json.GetValue("name");

                            string result = "Сейчас погода в Сертолово." + "\nСостояние: " + bbb.ToString() + ".\nТемпература: " + temp.ToString() + " С°.";
                            await botClient.SendTextMessageAsync(initedChatClass, result, ParseMode.Html);
                            return;

                        }
                        //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                    }

                }



                //запрос анекдота
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                    {
                        if (message.Text.ToLower().Contains("/запрос анекдота"))
                        {
                            anecdot = utilits.GetAnecdot();

                            await botClient.SendTextMessageAsync(initedChatAdmin, anecdot, ParseMode.Html);
                            return;
                        }
                        //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                    }

                }


                //сохранить анекдот

                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatAdmin != null && initedChatClass != null && anecdot != String.Empty && initedChatAdmin.Id == message.Chat.Id)
                    {
                        if (message.Text.ToLower().Contains("/сохранить анекдот"))
                        {
                            StreamWriter sw = new StreamWriter("./AnekdotOfDay.txt");
                            ////Write a line of text
                            sw.WriteLine(anecdot);
                            ////Write a second line of text
                            ////Close the file
                            sw.Close();
                            await botClient.SendTextMessageAsync(initedChatAdmin, "Анекдот \r " + anecdot + "\r сохранен ", ParseMode.Html);
                            return;
                        }
                        //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                    }
                }

                //получить сохраненный анекдот

                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatClass != null)
                    {
                        if (message.Text.ToLower().Contains("/анекдот"))
                        {
                            string path = "./AnekdotOfDay.txt";
                            bool fileExist = System.IO.File.Exists(path);
                            if (fileExist)
                            {
                                using (StreamReader reader = new StreamReader(path))
                                {
                                    string text = reader.ReadToEndAsync().Result;
                                    await botClient.SendTextMessageAsync(message.Chat, text);
                                }
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(initedChatAdmin, "Файла с анекдотом нет", ParseMode.Html);
                            }


                        }
                        //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                    }
                }


                //отправить в чат анекдот
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                    {
                        if (message.Text.ToLower().Contains("/отправить в чат анекдот"))
                        {

                            string path = "./AnekdotOfDay.txt";
                            bool fileExist = System.IO.File.Exists(path);
                            if (fileExist)
                            {
                                using (StreamReader reader = new StreamReader(path))
                                {
                                    string text = reader.ReadToEndAsync().Result;
                                    await botClient.SendTextMessageAsync(initedChatClass, text, ParseMode.Html);
                                }
                            }

                            else
                            {
                                await botClient.SendTextMessageAsync(initedChatAdmin, "Файла с анекдотом нет");
                            }

                        }
                        //     await botClient.DeleteMessageAsync(message.Chat.Id,message.MessageId); //удаляет сообщения
                    }

                }


                //отправить фото https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg


                //получить пробки
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null)
                    {
                        if (message.Text.ToLower().Contains("/пробки"))
                        {

                            // var asf = utilits.GetJam();
                            await botClient.SendPhotoAsync(message.Chat, photo: "https://static-maps.yandex.ru/1.x/?ll=30.226177,60.127171&size=450,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99",
     caption: "<b>Пробки у Сертолово</b>. <a href=\"https://yandex.ru/legal/maps_termsofuse/\">Условия использования карт</a>", ParseMode.Html, cancellationToken: cancellationToken);
                        }
                    }
                }

                //получить пробки
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                    {
                        message = update.Message;
                        if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                        {
                            if (message.Text.ToLower().Contains("/отправить пробки в чат"))
                            {

                                // var asf = utilits.GetJam();
                                await botClient.SendPhotoAsync(initedChatClass, photo: "https://static-maps.yandex.ru/1.x/?ll=30.226177,60.127171&size=450,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99",
          caption: "<b>Пробки у Сертолово</b>. <a href=\"https://yandex.ru/legal/maps_termsofuse/\">Условия использования карт</a>", ParseMode.Html, cancellationToken: cancellationToken);
                            }
                        }
                    }


                //
                //            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                //            {
                //                message = update.Message;
                //                if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                //                {
                //                    if (message.Text.ToLower().Contains("/получить картинку наса"))
                //                    {
                //                       await botClient.SendPhotoAsync(message.Chat, photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                //caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>", ParseMode.Html, cancellationToken: cancellationToken);
                //                    }

                //                }

                //            }


                //пробки https://static-maps.yandex.ru/1.x/?ll=30.22,60.12&size=650,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99



                //запустить таймер


                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    message = update.Message;
                    if (message.Text != null && initedChatAdmin != null && initedChatClass != null && initedChatAdmin.Id == message.Chat.Id)
                    {
                        if (message.Text.ToLower().Contains("/запустить таймер"))
                        {
                            Thread myThread3 = new Thread(() => Print());
                            myThread3.Start();
                        }
                    }
                }






                async Task PrintAsync()
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Начало метода PrintAsync");
                    await Task.Run(() => Print());                // выполняется асинхронно
                    await botClient.SendTextMessageAsync(message.Chat, "Конец метода PrintAsync");


                }




                void Print()
                {



                    DateTime eightHour = new DateTime();
                    eightHour = eightHour.AddHours(8);
                    DateTime nineHour = new DateTime();
                    nineHour = nineHour.AddHours(9);
                    DateTime tenHour = new DateTime();
                    tenHour = tenHour.AddHours(10);
                    DateTime elevenHour = new DateTime();
                    elevenHour = elevenHour.AddHours(11);
                    DateTime twelveHour = new DateTime();
                    twelveHour = twelveHour.AddHours(12);
                    DateTime thirteenHour = new DateTime();
                    thirteenHour = thirteenHour.AddHours(13);
                    DateTime fourteenHour = new DateTime();
                    fourteenHour = fourteenHour.AddHours(14);
                    DateTime fifteenHour = new DateTime();
                    fifteenHour = fifteenHour.AddHours(15);
                    DateTime sixteenHour = new DateTime();
                    sixteenHour = sixteenHour.AddHours(16);
                    DateTime seventeenHour = new DateTime();
                    seventeenHour = seventeenHour.AddHours(17);
                    DateTime eighteenHour = new DateTime();
                    eighteenHour = eighteenHour.AddHours(18);
                    DateTime nineteenHour = new DateTime();
                    nineteenHour = nineteenHour.AddHours(19);
                    DateTime twentyHour = new DateTime();
                    twentyHour = twentyHour.AddHours(20);
                    DateTime twentyOneHour = new DateTime();
                    twentyOneHour = twentyOneHour.AddHours(21);
                    DateTime twentyTwoHour = new DateTime();
                    twentyTwoHour = twentyTwoHour.AddHours(22);
                    DateTime twentyThreeHour = new DateTime();
                    twentyThreeHour = twentyThreeHour.AddHours(23);
                    DateTime twentyFourHour = new DateTime();
                    twentyFourHour = twentyFourHour.AddHours(0);


                    DateTime dateTimeNowThead;


                    dateTimeNowThead = new DateTime();
                    while (true)
                    {
                        dateTimeNowThead = DateTime.Now;

                        //пробки с восьми до десяти
                        if (eightHour.TimeOfDay.Ticks < dateTimeNowThead.TimeOfDay.Ticks && dateTimeNowThead.TimeOfDay.Ticks < tenHour.TimeOfDay.Ticks)
                        //if (seventeenHour.TimeOfDay.Ticks < dateTimeNowThead.TimeOfDay.Ticks && dateTimeNowThead.TimeOfDay.Ticks < nineteenHour.TimeOfDay.Ticks)
                        {
                            botClient.SendTextMessageAsync(initedChatAdmin, "сейчас от восьми до десяти");

                            if (initedChatAdmin != null && initedChatClass != null)
                            {
                                string path = "./probky_vosem_desyat.txt";
                                string text = String.Empty;
                                bool fileExist = System.IO.File.Exists(path);
                                if (fileExist)
                                {
                                    StreamReader reader = new StreamReader(path);
                                    
                                        text = reader.ReadToEndAsync().Result;
                                    reader.Close();
                                        botClient.SendTextMessageAsync(initedChatAdmin, text);

                                    if (text.Contains(dateTimeNowThead.Date.Ticks.ToString()))
                                    { 
                                    
                                    }
                                    else
                                    {
                                        botClient.SendPhotoAsync(initedChatClass, photo: "https://static-maps.yandex.ru/1.x/?ll=30.226177,60.127171&size=450,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99",
             caption: "<b>Пробки у Сертолово</b>. <a href=\"https://yandex.ru/legal/maps_termsofuse/\">Условия использования карт.</a>", ParseMode.Html, cancellationToken: cancellationToken);

                                        StreamWriter sw = new StreamWriter("./probky_vosem_desyat.txt");
                                        ////Write a line of text
                                        sw.WriteLine(dateTimeNowThead.Date.Ticks.ToString());
                                        sw.WriteLine(true.ToString());
                                        ////Write a second line of text
                                        ////Close the file
                                        sw.Close();
                                    }
                                    
                                }

                                else
                                {
                                    botClient.SendTextMessageAsync(initedChatAdmin, "Файла с даннами  - пробки восемь десять - нет", ParseMode.Html);

                                    StreamWriter sw = new StreamWriter("./probky_vosem_desyat.txt");
                                    ////Write a line of text
                                    sw.WriteLine(dateTimeNowThead.Ticks.ToString());
                                    sw.WriteLine(true.ToString());
                                    ////Write a second line of text
                                    ////Close the file
                                    sw.Close();

                                    botClient.SendPhotoAsync(initedChatClass, photo: "https://static-maps.yandex.ru/1.x/?ll=30.226177,60.127171&size=450,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99",
    caption: "<b>Пробки у Сертолово</b>. <a href=\"https://yandex.ru/legal/maps_termsofuse/\">Условия использования карт.</a>", ParseMode.Html, cancellationToken: cancellationToken);

                                }
                            }
                        }


                      
                        //toDo




                    }
                }

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        //Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        //{
        //    var ErrorMessage = exception switch
        //    {
        //        ApiRequestException apiRequestException
        //            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        //        _ => exception.ToString()
        //    };

        //    Console.WriteLine(ErrorMessage);
        //    return Task.CompletedTask;
        //}


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);



            dateTimeNow = DateTime.Now;



            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );






            Console.ReadLine();
        }
    }
}