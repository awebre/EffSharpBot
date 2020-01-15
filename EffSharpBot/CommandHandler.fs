module EffSharpBot.CommandHandler
open Discord.Commands

type CommandHandler(initSettings:Program.Settings initCommandService:CommandService) =
    let settings = initSettings;
    