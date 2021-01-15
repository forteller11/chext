using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;

namespace chext.Discord
{
    //todo optimization where only allocate new embed class if fields are changed and it's necessary
    //todo also don't always clear fields?
    public class EmbededDrawer
    {
        public readonly EmbedBuilder Builder;
        public readonly IMessageChannel Channel;
        
        private Task<IUserMessage> _messageTask;
        private bool _hasDrawnBefore;

        public EmbededDrawer(IMessageChannel channel)
        {
            Builder = new EmbedBuilder();
            Channel = channel;
        }

        public async ValueTask Draw()
        {
            if (_hasDrawnBefore)
            {
                Program.DebugLog("draw modify start");
                await _messageTask; //makes sure you have the result of the user message returned from the first draw
                await _messageTask.Result.ModifyAsync(properties => { properties.Embed = Builder.Build(); });
            }
            else
            {
                Program.DebugLog("draw create start");
                _messageTask = Channel.SendMessageAsync(null, false, Builder.Build());
                _hasDrawnBefore = true;
                await _messageTask;
            }
            Program.DebugLog("draw done");
        }
    }
}