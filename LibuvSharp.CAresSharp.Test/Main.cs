using System;
using LibuvSharp;
using CAresSharp;

namespace LibuvSharp.CAresSharp.Test
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			CAres.Init();
			
			var cares = Loop.Default.DefaultCAresChannel();
			
			string host = "www.google.de";
			if (args.Length > 0) {
				host = args[0];	
			}
			
			cares.Resolve(host, (e, h) => {
				if (e != null) {
					Console.WriteLine(e);
					return;
				}
				
				foreach (var ip in h.IPAddresses) {
					Console.WriteLine(ip);	
				}
			});
			
			Loop.Default.Run();
			
			CAres.Cleanup();
		}
	}
}

