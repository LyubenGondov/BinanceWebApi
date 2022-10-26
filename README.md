# BinanceWebApi
First you can start ConsumingBinanceWebApi. It is made with code first approach. It is NET CORE web application(MVC). There is no conditional for pressing the 
button to save information in dadabase.
I do not use a lot of checkings because it will take time. It is made just to see that the wss api is consummed. When you choose
the symbol and after the app stop to load, you can see the results in database. As I said I use migrations(Add-Migration <name> and Update-Database). Firstly befor starting the project you shoud do this things.
In the class ConsummingClass the magic happens. There you can find the logic. After that I have one new project called NewWeBApi(not that good name). There I made two methods 
first for averige price for last 24 hours and the second for SMA. SMA I am not sure that the formulas and desired things are correct. As I said I do not have enough time but
I think that the purpose of this project is to show my ability for coding not the whole task. I forgot to mention that I use >net core 3.1. So all used libralies are for old version of .NET CORE(3.1).
The last project that was a console application is not done. It is just consumming NewWebApi and depends on the given comand in cmd to show respectively the averige price and SAM
