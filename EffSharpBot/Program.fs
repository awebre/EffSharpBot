open Discord

// Learn more about F# at http://fsharp.org

open Discord.WebSocket
open System
open System.Threading.Tasks
open FSharp.Data

type Settings = JsonProvider<"appsettings.json">
let settings = Settings.GetSample();

let log = Func<LogMessage, Task>(fun (logMessage:LogMessage) -> 
    Console.WriteLine(logMessage.ToString())
    Task.CompletedTask)

let messageRecieved = Func<SocketMessage, Task>(fun (message:SocketMessage) ->
    if message.Content.Equals("!Ping")
    then message.Channel.SendMessageAsync("Pong!") :> Task
    else Task.CompletedTask)

[<EntryPoint>]
let main argv =
    async {
                
        let client = new DiscordSocketClient()
        client.add_Log(log)
        
        do! client.LoginAsync(TokenType.Bot, settings.DiscordToken) |> Async.AwaitTask
        do! client.StartAsync() |> Async.AwaitTask
        
        client.add_MessageReceived(messageRecieved)
        
        do! Task.Delay(-1) |> Async.AwaitTask
    } |> Async.RunSynchronously
    0
    
