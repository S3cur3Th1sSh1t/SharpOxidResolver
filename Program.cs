//
// Copyright (c) Oxid Resolver. All rights reserved.
// 
//
// Licensed under the Non-Profit OSL. See LICENSE file in the project root for full license information.
//


using OxidResolver.RPC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.DirectoryServices;
using System.Net.NetworkInformation;

namespace OxidResolver
{



	public class Program
	{

		public static void Main(string[] args)
		{
			string outbindings;
			string host;

			if (args == null || args.Length == 0)
			{
				List<string> ComputerNames = new List<string>();
				System.DirectoryServices.ActiveDirectory.Domain domain = System.DirectoryServices.ActiveDirectory.Domain.GetCurrentDomain();

				string currentdom = "LDAP://" + domain.ToString();
				DirectoryEntry entry = new DirectoryEntry(currentdom);
				DirectorySearcher mySearcher = new DirectorySearcher(entry);
				mySearcher.Filter = ("(objectClass=computer)");
				mySearcher.SizeLimit = int.MaxValue;
				mySearcher.PageSize = int.MaxValue;

				foreach (SearchResult resEnt in mySearcher.FindAll())
				{
					string ComputerName = resEnt.GetDirectoryEntry().Name;
					if (ComputerName.StartsWith("CN="))
						ComputerName = ComputerName.Remove(0, "CN=".Length);
					ComputerNames.Add(ComputerName);
				}

				mySearcher.Dispose();
				entry.Dispose();

				foreach (string computer in ComputerNames)
				{
					Console.WriteLine("Getting bindings for " + computer + ":");
					Console.WriteLine("");
					PingReply pingReply;
					bool error = false;
					using (var ping = new Ping())
					{
						try
						{
							pingReply = ping.Send(computer);
						}
						catch
                        {
							Console.WriteLine("No DNS");
							pingReply = ping.Send("localhost");
							error = true;
						}
					}
					if (pingReply.Status == IPStatus.Success && error != true)
					{
						outbindings = GetCsvData(computer);
						Console.WriteLine(outbindings);
						Console.WriteLine("");
						Console.WriteLine("");
					}
                    else
                    {
						Console.WriteLine("Computer not accessible");
						Console.WriteLine("");
						Console.WriteLine("");
					}
					
				}
				
			}
			else
			{
				host = args[0];
				outbindings = GetCsvData(host);
				Console.WriteLine(outbindings);

			}
	    }
		public string Name = "oxidbindings";
		public string Description = "List all IP of the computer via the Oxid Resolver (part of DCOM). No authentication. Used to find other networks such as the one used for administration.";

		protected string GetCsvHeader()
		{
			return "Computer\tBinding";
		}

		public static string GetCsvData(string computer)
		{
			StringBuilder sb = new StringBuilder();
			DisplayAdvancement(computer, "Connecting to Oxid Resolver");
			List<string> bindings;
			var oxid = new OxidBindings();
			int res = oxid.ServerAlive2(computer, out bindings);
			if (res != 0)
			{
				DisplayAdvancement(computer, "error " + res);
				sb.Append(computer);
				sb.Append("\tError " + res);
			}
			else
			{
				foreach (var binding in bindings)
				{
					if (sb.Length != 0)
						sb.Append("\r\n");
					sb.Append(computer);
					sb.Append("\t");
					sb.Append(binding);
				}
			}
			return sb.ToString();
		}

		public static void DisplayAdvancement(string computer, string data)
		{
			string value = "[" + DateTime.Now.ToLongTimeString() + "] " + data;
			Console.WriteLine(value);
			Trace.WriteLine(value);
		}

	
	}
}


