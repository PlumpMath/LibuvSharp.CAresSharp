using System;
using LibuvSharp;
using CAresSharp;
using System.Collections.Generic;

namespace LibuvSharp.CAresSharp
{
	public static class LoopExtensions
	{
		public static CAresChannel CAresChannel(this Loop loop)
		{
			Dictionary<int, Poll> dict = new Dictionary<int, Poll>();
			
			var dns = new CAresChannel(new CAresChannelOptions() {
				SocketCallback = (channel, socket, read, write) => {
					if (read | write) {
						var poll = new Poll(loop, socket);
						poll.Start(PollEvent.Read, (@event) => {
							channel.Process(socket, -1);
						});
						dict[socket] = poll;
					} else {
						var poll = dict[socket];
						poll.Close();
						dict.Remove(socket);
					}
				}
			});
			return dns;
		}
	}
}

