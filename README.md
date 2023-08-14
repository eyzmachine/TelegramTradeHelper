
RUS:
Общая информация:
Небольшая утилита для пересылки личных сообщений в Телеграм

Важные замечания:
1. Трейды от корейцев не пишутся в лог ввиду какого-то ограничения со стороны GGG, имейте это ввиду. Как вариант, можно оставить все сообщения и бежать к компу, если пришло в игру, а в телегу не пришло
2. Если есть иконка лучше - присылайте, я только за

Инструкция:
1. Написать в Telegram BotFather-у (https://t.me/BotFather) команду /start
1.1 Написать ему же команду /newbot
1.2 Следующим сообщением он предложит наименовать бота, любое
1.3 Следующим сообщением нужно набрать @ник бота, тоже любой, лишь бы свободен и оканчивался на bot
1.4 Скопировать токен, который BotFather вам пришлёт, будет выглядеть примерно как 6308186963:AAEwDFcOI1ngEch6iZykCwBtxPN-kPsLT24
2. Открыть любым блокнотом файл TradeHelper.exe.config рядом с файлом запуска и вставить преусловутый токен в тег с ключом telegramToken
3. В этом же файл в тег с ключом pathToLogFile вставить полный путь до логов Client.txt (обычно находится в папке logs игры)
4. Если клиент русский, то поменять язык language на значение ru (важно, если надо только трейд сообщения доставать из лички)
5. Если нужны все сообщения, а не только трейд, то в тег с ключом allMesages вставить true вместо false
6. Сохранить конфиг и запустить программу
7. Если всё вбито правильно и токен рабочий, то после нажатия "Проверить коннект" надпись поменяется на Коннект есть
7.1 Если коннекта нет, то перепроверить токен
8. Нажать "Начало работы бота" и прописать в раннее созданном боте команду /start . Если всё хорошо, придёт ответное сообщение о том, что бот работает
9. Чтобы не начинать каждый раз работу с команды старт, можно получить UserId и UserName с помощью команды /getid, либо через бота https://t.me/username_to_id_bot , и вписать их в конфиг с оответствующие теги

--------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------

ENG:
Info:
Small app for sending pm messages from PoE client to Telegram

IMPORTANT:
1. Trades from koreans are not going into log for some reason from GGG, FYI. You can enable all messages in config and just coming to PC when there is message in PoE and not in tg
2. If you got better icon say me

Instruction:
1. Write command /start to BotFather in Telegram (https://t.me/BotFather)
1.1 Write command /newbot
1.2 Next message you have to name your bot
1.3 Next message you have to choose @nick of your bot
1.4 BotFather will send you token like 6308186963:AAEwDFcOI1ngEch6iZykCwBtxPN-kPsLT24, copy it
2. Open with and notepad TradeHelper.exe.config file near execution file and paste token into telegramToken tag
3. In same file paste full path to log file Client.txt (usually this file in "Logs" folder)
4. If for some reason your client is russian, you have to change tag value Language to ru (important for only trade messages from pm)
5. If you want all messages, not only trade, set true in allMesages tag value
6. Save config and run exe
7. If you did everything right and token is valid, then after pressing Check connection button you will see Connection is fine label
7.1 If connection failed then check token
8. Press Start working, then type in your bot /start. If everything alright you will get message that bot is working.
9. If you dont want to type /start always after app launch you can get your UserId and UserName with /getid command or with https://t.me/username_to_id_bot bot and paste it in same name tag values in config
