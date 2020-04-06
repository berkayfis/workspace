using System;
using System.Linq;
using System.Collections.Generic;
using WebApplication14.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YamlDotNet.Serialization;
using System.IO;

namespace WebApplication14.Helper
{
    public static class DbHelper
    {
        public static void MigrateDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AraProjeContext>();

                context.Database.Migrate();

                using (var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "configuration.yaml")))
                {
                    var deserializer = new DeserializerBuilder().Build();

                    var config = deserializer.Deserialize<DbConfigModel>(reader);

                    if (!context.AkademikPersonel.Any())
                    {
                        var akademikPersoneller = new List<AkademikPersonel>();

                        foreach (var akademikPersonelConfig in config.AkademikPersoneller)
                        {
                            akademikPersoneller.Add(new AkademikPersonel
                            {
                                Ad = akademikPersonelConfig.Ad,
                                Soyad = akademikPersonelConfig.Soyad,
                                Anabilimdali = akademikPersonelConfig.AnaBilimDali,
                                Unvan = akademikPersonelConfig.Unvan,
                                Kisaltma = akademikPersonelConfig.Kisaltma,
                                KullaniciAdi = akademikPersonelConfig.KullaniciAdi,
                                Sifre = akademikPersonelConfig.Sifre,
                                MessageId = Guid.NewGuid()
                            });
                        }

                        context.AkademikPersonel.AddRange(akademikPersoneller);

                        context.SaveChanges();

                        config.AkademikPersoneller.Where(x => x.IsKoordinator).ToList().ForEach(x =>
                        {
                            context.ProjeKoordinatoru.Add(new ProjeKoordinatoru
                            {
                                AkademisyenId = akademikPersoneller.First(x => x.KullaniciAdi == x.KullaniciAdi).Id
                            });
                        });

                        context.SaveChanges();
                    }

                    if (!context.Ogrenci.Any())
                    {
                        var ogrenciler = new List<Ogrenci>();

                        foreach (var ogrenciConfig in config.Ogrenciler)
                        {
                            ogrenciler.Add(new Ogrenci
                            {
                                OgrenciNo = ogrenciConfig.OgrenciNo,
                                Ad = ogrenciConfig.Ad,
                                Soyad = ogrenciConfig.Soyad,
                                MessageId = Guid.NewGuid()
                            });
                        }

                        context.Ogrenci.AddRange(ogrenciler);

                        context.SaveChanges();
                    }

                    if (!context.Takvim.Any())
                    {
                        context.Takvim.Add(new Takvim
                        {
                            Form2 = DateTime.Now,
                            Toplanti = DateTime.Now
                        });

                        context.SaveChanges();
                    }
                }
            }
        }
    }

    internal class DbConfigModel
    {
        [YamlMember(Alias = "Akademik_Personeller", ApplyNamingConventions = false)]
        public List<AkademikPersonelConfig> AkademikPersoneller { get; set; }

        public List<OgrenciConfig> Ogrenciler { get; set; }
    }

    internal class AkademikPersonelConfig
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [YamlMember(Alias = "Ana_Bilim_Dali", ApplyNamingConventions = false)]
        public string AnaBilimDali { get; set; }
        public string Unvan { get; set; }
        public string Kisaltma { get; set; }
        [YamlMember(Alias = "Kullanici_Adi", ApplyNamingConventions = false)]
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        [YamlMember(Alias = "Koordinator_Mu", ApplyNamingConventions = false)]
        public bool IsKoordinator { get; set; }
    }

    internal class OgrenciConfig
    {
        [YamlMember(Alias = "Ogrenci_No", ApplyNamingConventions = false)]
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}