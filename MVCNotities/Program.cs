using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNotities
{
    /*
     * Notities
        Maak een website waar notities kunnen opgeslagen worden (CRUD).
        Een notitie heeft een titel, datum en beschrijving en eventueel nog andere dingen naar eigen smaak.
        Op de index staan alle notities gesorteerd op datum, en ook onderverdeeld op maand.

        Voorbeeld:

        Maart 2021
        Boodschappen - 19/03/2021
        Eieren kopen.
        Tandarts - 27/03/2021
        Ga naar de tandarts.
        April 2021
        Sollicitatie - 16/04/2021
        Op sollicitatie bij Multimedi.
        Juni 2021
        Corona Vaccin - 28/06/2021
        Ga corona vaccin halen.

     */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
