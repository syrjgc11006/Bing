﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.BankCards
{
    /// <summary>
    /// 银行卡规则生成器
    /// </summary>
    public class BankCardRuleBuilder
    {
        /// <summary>
        /// 银行卡规则
        /// </summary>
        internal static List<CardRule> CardRules=new List<CardRule>();

        /// <summary>
        /// 是否初始化
        /// </summary>
        internal static bool IsInit = false;

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="name">银行名称</param>
        /// <param name="code">银行编码</param>
        /// <param name="action">规则操作</param>
        public static void AddRule(string name, string code, Action<CardRule> action)
        {
            CardRule rule=new CardRule(name,code);
            action.Invoke(rule);
            CardRules.Add(rule);
        }

        /// <summary>
        /// 获取银行卡信息
        /// </summary>
        /// <param name="card">银行卡号</param>
        /// <returns></returns>
        public static BankInfo GetBankInfo(string card)
        {
            if (!IsInit)
            {
                InitRule();
            }
            foreach (var cardRule in CardRules)
            {
                if (!cardRule.AllRules.Any(rule => Regex.IsMatch(card, rule))) continue;
                BankInfo info = new BankInfo(card)
                {
                    Type = cardRule.GetCardType(card),
                    CardNumber = card,
                    Code = cardRule.Code,
                    Name = cardRule.Name
                };
                return info;
            }
            return null;
        }

        /// <summary>
        /// 初始化规则
        /// </summary>
        internal static void InitRule()
        {
            #region 邮政银行
            // 1-3
            AddRule("邮储银行", "100", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(621096|621098|622150|622151|622181|622188|622199|955100|621095|620062|621285|621798|621799|621797|620529|621622|621599|621674|623218|623219)\\d{13}$")
                    .AddRule(CardType.DebitCard, "^(62215049|62215050|62215051|62218850|62218851|62218849)\\d{11}$")
                    .AddRule(CardType.CreditCard, "^(622812|622810|622811|628310|625919)\\d{10}$");
            });

            #endregion

            #region 工商银行
            // 4-13
            AddRule("工商银行", "102", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(620200|620302|620402|620403|620404|620406|620407|620409|620410|620411|620412|620502|620503|620405|620408|620512|620602|620604|620607|620611|620612|620704|620706|620707|620708|620709|620710|620609|620712|620713|620714|620802|620711|620904|620905|621001|620902|621103|621105|621106|621107|621102|621203|621204|621205|621206|621207|621208|621209|621210|621302|621303|621202|621305|621306|621307|621309|621311|621313|621211|621315|621304|621402|621404|621405|621406|621407|621408|621409|621410|621502|621317|621511|621602|621603|621604|621605|621608|621609|621610|621611|621612|621613|621614|621615|621616|621617|621607|621606|621804|621807|621813|621814|621817|621901|621904|621905|621906|621907|621908|621909|621910|621911|621912|621913|621915|622002|621903|622004|622005|622006|622007|622008|622010|622011|622012|621914|622015|622016|622003|622018|622019|622020|622102|622103|622104|622105|622013|622111|622114|622017|622110|622303|622304|622305|622306|622307|622308|622309|622314|622315|622317|622302|622402|622403|622404|622313|622504|622505|622509|622513|622517|622502|622604|622605|622606|622510|622703|622715|622806|622902|622903|622706|623002|623006|623008|623011|623012|622904|623015|623100|623202|623301|623400|623500|623602|623803|623901|623014|624100|624200|624301|624402|623700|624000)\\d{12}$")
                    .AddRule(CardType.DebitCard,
                        "^(622200|622202|622203|622208|621225|620058|621281|900000|621558|621559|621722|621723|620086|621226|621618|620516|621227|621288|621721|900010|623062|621670|621720|621379|621240|621724|621762|621414|621375|622926|622927|622928|622929|622930|622931|621733|621732|621372|621369|621763)\\d{13}$")
                    .AddRule(CardType.DebitCard,
                        "^(402791|427028|427038|548259|621376|621423|621428|621434|621761|621749|621300|621378|622944|622949|621371|621730|621734|621433|621370|621764|621464|621765|621750|621377|621367|621374|621731|621781)\\d{10}$")
                    .AddRule(CardType.DebitCard, "^(9558)\\d{15}$")
                    .AddRule(CardType.CreditCard, "^(370246|370248|370249|370247|370267|374738|374739)\\d{9}$")
                    .AddRule(CardType.CreditCard, "^(427010|427018|427019|427020|427029|427030|427039|438125|438126|451804|451810|451811|458071|489734|489735|489736|510529|427062|524091|427064|530970|530990|558360|524047|525498|622230|622231|622232|622233|622234|622235|622237|622239|622240|622245|622238|451804|451810|451811|458071|628288|628286|622206|526836|513685|543098|458441|622246|544210|548943|356879|356880|356881|356882|528856|625330|625331|625332|622236|524374|550213|625929|625927|625939|625987|625930|625114|622159|625021|625022|625932|622889|625900|625915|625916|622171|625931|625113|625928|625914|625986|625925|625921|625926|625942|622158|625917|625922|625934|625933|625920|625924|625017|625018|625019)\\d{10}$")
                    .AddRule(CardType.CreditCard, "^(45806|53098|45806|53098)\\d{11}$")
                    .AddRule(CardType.QuasiCreditCard, "^(622210|622211|622212|622213|622214|622220|622223|622225|622229|622215|622224)\\d{10}$")
                    .AddRule(CardType.PrepaidCard, "^(620054|620142|620184|620030|620050|620143|620149|620124|620183|620094|620186|620148|620185)\\d{10}$")
                    .AddRule(CardType.PrepaidCard, "^(620114|620187|620046)\\d{13}$");
            });

            #endregion

            #region 农业银行
            //14-18
            AddRule("农业银行","103", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(622841|622824|622826|622848|620059|621282|622828|622823|621336|621619|622821|622822|622825|622827|622845|622849|623018|623206|621671|622840|622843|622844|622846|622847|620501)\\d{13}$")
                    .AddRule(CardType.DebitCard, "^(95595|95596|95597|95598|95599)\\d{14}$")
                    .AddRule(CardType.DebitCard, "^(103)\\d{16}$")
                    .AddRule(CardType.CreditCard,
                        "^(403361|404117|404118|404119|404120|404121|463758|519412|519413|520082|520083|552599|558730|514027|622836|622837|628268|625996|625998|625997|622838|625336|625826|625827|544243|548478|628269)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard, "^(622820|622830)\\d{10}$");
            });

            #endregion

            #region 中国银行
            // 19-25
            AddRule("中国银行", "104", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(621660|621661|621662|621663|621665|621667|621668|621669|621666|456351|601382|621256|621212|621283|620061|621725|621330|621331|621332|621333|621297|621568|621569|621672|623208|621620|621756|621757|621758|621759|621785|621786|621787|621788|621789|621790|622273|622274|622771|622772|622770|621741|621041)\\d{13}$")
                    .AddRule(CardType.DebitCard,
                        "^(621293|621294|621342|621343|621364|621394|621648|621248|621215|621249|621231|621638|621334|621395|623040|622348)\\d{10}$")
                    .AddRule(CardType.CreditCard,
                        "^(625908|625910|625909|356833|356835|409665|409666|409668|409669|409670|409671|409672|512315|512316|512411|512412|514957|409667|438088|552742|553131|514958|622760|628388|518377|622788|628313|628312|622750|622751|625145|622479|622480|622789|625140|622346|622347)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard,
                        "^(518378|518379|518474|518475|518476|524865|525745|525746|547766|558868|622752|622753|622755|524864|622757|622758|622759|622761|622762|622763|622756|622754|622764|622765|558869|625905|625906|625907|625333)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard, "^(53591|49102|377677)\\d{11}$")
                    .AddRule(CardType.PrepaidCard,
                        "^(620514|620025|620026|620210|620211|620019|620035|620202|620203|620048|620515|920000)\\d{10}$")
                    .AddRule(CardType.PrepaidCard, "^(620040|620531|620513|921000|620038)\\d{13}$");
            });

            #endregion

            #region 建设银行
            // 26-33
            AddRule("建设银行", "105", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(621284|436742|589970|620060|621081|621467|621598|621621|621700|622280|622700|623211|623668)\\d{13}$")
                    .AddRule(CardType.DebitCard,
                        "^(421349|434061|434062|524094|526410|552245|621080|621082|621466|621488|621499|622966|622988|622382|621487|621083|621084|620107)\\d{10}$")
                    .AddRule(CardType.DebitCard,
                        "^(436742193|622280193)\\d{10}$")
                    .AddRule(CardType.CreditCard,
                        "^(553242)\\d{12}$")
                    .AddRule(CardType.CreditCard, "^(625362|625363|628316|628317|356896|356899|356895|436718|436738|436745|436748|489592|531693|532450|532458|544887|552801|557080|558895|559051|622166|622168|622708|625964|625965|625966|628266|628366|622381|622675|622676|622677)\\d{10}$")
                    .AddRule(CardType.CreditCard,
                        "^(5453242|5491031|5544033)\\d{11}$")
                    .AddRule(CardType.QuasiCreditCard, "^(622725|622728|436728|453242|491031|544033|622707|625955|625956)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard, "^(53242|53243)\\d{11}$");
            });

            #endregion

            #region 交通银行
            // 34-40
            AddRule("交通银行", "301", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(622261|622260|622262|621002|621069|621436|621335)\\d{13}$")
                    .AddRule(CardType.DebitCard,
                        "^(620013)\\d{10}$")
                    .AddRule(CardType.DebitCard,
                        "^(405512|601428|405512|601428|622258|622259|405512|601428)\\d{11}$")
                    .AddRule(CardType.CreditCard,
                        "^(49104|53783)\\d{11}$")
                    .AddRule(CardType.CreditCard,
                        "^(434910|458123|458124|520169|522964|552853|622250|622251|521899|622253|622656|628216|622252|955590|955591|955592|955593|628218|625028|625029)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard,
                        "^(622254|622255|622256|622257|622284)\\d{10}$")
                    .AddRule(CardType.PrepaidCard, "^(620021|620521)\\d{13}$");
            });

            #endregion

            #region 招商银行
            // 41-46
            AddRule("招商银行", "308", x =>
            {
                x.AddRule(CardType.DebitCard,
                        "^(402658|410062|468203|512425|524011|622580|622588|622598|622609|95555|621286|621483|621485|621486|621299)(\\d{10}|\\d{11})$")
                    .AddRule(CardType.DebitCard,
                        "^(690755)\\d{9}$")
                    .AddRule(CardType.DebitCard,
                        "^(690755)\\d{12}$")
                    .AddRule(CardType.QuasiCreditCard,
                        "^(356885|356886|356887|356888|356890|439188|439227|479228|479229|521302|356889|545620|545621|545947|545948|552534|552587|622575|622576|622577|622578|622579|545619|622581|622582|545623|628290|439225|518710|518718|628362|439226|628262|625802|625803)\\d{10}$")
                    .AddRule(CardType.QuasiCreditCard,
                        "^(370285|370286|370287|370289)\\d{9}$")
                    .AddRule(CardType.PrepaidCard,
                        "^(620520)\\d{13}$");
            });

            #endregion
        }
    }
}
