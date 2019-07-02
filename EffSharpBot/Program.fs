// Learn more about F# at http://fsharp.org

open Discord
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
        
        client.LoginAsync(TokenType.Bot, settings.DiscordToken).Wait()
        client.StartAsync().Wait()
        
        client.add_MessageReceived(messageRecieved)
        
        Task.Delay(-1).Wait()
    } |> Async.RunSynchronously
    0
    
