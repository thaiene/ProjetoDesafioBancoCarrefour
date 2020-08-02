using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatBotTelegramDIO
{
    class BuscarSiteCarrefour
    {
        public string nome { get; set; }
        public string preco { get; set; }
        public string link { get; set; }
        public BuscarSiteCarrefour(string mensagem)
        {
            var site = new HtmlWeb().Load("https://busca.carrefour.com.br/busca?q=" + mensagem);
            var produto = site.DocumentNode.SelectSingleNode("//div[@class='nm-product-info']/h2[@class='nm-product-name']");
            var valor = site.DocumentNode.SelectSingleNode("//div[@class='nm-price-container']/span[@class='nm-price-value']");
            var linkProduto = site.DocumentNode.SelectSingleNode("//div[@class='nm-product-info']/h2[@class='nm-product-name']/a").Attributes["href"].Value;
            if (valor != null && produto != null)
            {
                preco = valor.InnerText.Trim();
                nome = produto.InnerText.Trim();
                link = linkProduto.Trim();
            }
        }
    }
}
