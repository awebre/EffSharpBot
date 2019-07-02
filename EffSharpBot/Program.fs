open Discord

// Learn more about F# at http://fsharp.org

open Discord.WebSocket
open System
open System.Threading.Tasks

let log = Func<LogMessage, Task>(fun (logMessage:LogMessage) -> 
    Console.WriteLine(logMessage.ToString())
    Task.CompletedTask)

[<EntryPoint>]
let main argv =
    async {
        let client = new DiscordSocketClient()
        client.add_Log(log)
        
        return 0
    } |> Async.RunSynchronously
