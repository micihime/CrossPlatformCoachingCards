using CoachingCards.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CoachingCards.Services
{
    public static class CardService
    {
        static SQLiteAsyncConnection db;

        static List<Card> cards = new List<Card>
            {
                new Card{ Heading = "1. Přijmi, změň, opusť", Text = "Potřebuješ se rozhodnout? Zkus si pomoct trojicí reakcí: přijmi, změň, opusť.\nPŘIJMI situaci takovou, jaká je.\nZMĚŇ sebe, svůj přístup, svůj úhel pohledu, své myšlení.\nOPUSŤ situaci, která pro tebe nemá jiné vhodné řešení.\n\nJakékoliv rozhodnutí se stane mnohem snazší a sebevědomější. Například: Zavádění novinky v práci. Nadávat na vedení ani na systém ti nepomůže. Buď: \n1. novinku prostě přijmeš takovou, jaká je,\n2. nebo změníš svůj přístup a z původní nevýhody pro sebe uděláš výhodu,\n3. nebo je to pro tebe neakceptovatelné, tudíž odcházíš.", Action = "Akce: Použij na své aktuální dilema.", IsLeft = true},
                new Card{ Heading = "2. Užitečné vs. zajímavé", Text = "Pro rychlou a k výsledku vedoucí komunikaci a pro efektivní uvažování rozlišuj užitečné od zajímavého a věnuj se jenom užitečnému. Užitečné je to, co vede k vytyčenému výsledku. Zajímavé je to, co cestu k výsledku prodlužuje.\n\nNapříklad: Marie kamarádce podrobně vypráví, jak si na kole zranila koleno (orientace na zajímavé). Ale když jde Marie k lékaři, tak mu sdělí: „Bolí mě koleno. Tady.“ (Orientace na užitečné).\n\nZefektivníš komunikaci i přemýšlení a ušetříš čas.\nNapříklad: Pracovní porady zkrátíš o 50 % až 75 % času, zaměříš se na výsledek, zbavíš se zdržovacích odboček atd.", Action = "Akce: Vyzkoušej jeden den vědomě rozlišovat užitečné od zajímavého a dál se věnuj pouze užitečnému.", IsLeft = true},
                new Card{ Heading = "3. Sféry vlivu",Text =  "Ovlivňuj to, co ovlivnit můžeš. To je to, co tvoříš. To jsi ty.\n\nNeztrácej energii tím, co ovlivnit nemůžeš.\n\nA hlavně se soustřeď na to, abys dokázal tyto dvě kategorie rozlišit.\n\nNapříklad: Svého šéfa, politika, rodiče ovlivnit nemůžeš. Můžeš ale ovlivnit svůj postoj k nim, svůj úhel pohledu.", Action = "Akce: Jeden den veškeré situace kolem sebe posuzuj, zda jsou přímo tebou ovlivnitelné, či nikoliv. Věnuj se jen těm, které můžeš přímo ovlivnit. Všimni si množství ušetřené energie a času.", IsLeft = true},
                new Card{ Heading = "4. Tady a teď se zdroji, které máš", Text = "Uvědom si, že vše, co v životě činíš, děláš nejlépe, jak umíš. Tady a teď se zdroji, které máš. Věř v sebe, ve své schopnosti a nereviduj svá minulá rozhodnutí.\n\nJakýkoliv pohled zpět je vždy „po bitvě generálem“. Pouč se z historie.", Action = "Akce: Vezmi jedno své životní rozhodnutí typu: jak by se odehrával můj život, kdyby to tehdy bylo jinak. Tentokrát si ale věcně a konkrétně uvědom, jaké tehdy byly tvé zdroje (což jsou i informace), a ruku na srdce, jaké by bylo tvé rozhodnutí znovu za stejných okolností?", IsLeft = true},
                new Card{ Heading = "5. Vidíme mozkem", Text = "Očima ve skutečnosti nevidíme. Na svět se díváme mozkem.\n\nVše, co k nám přichází, projde skrz síto našich myšlenek, zkušeností a návyků. Klasickým příkladem je sklenice, která může být pro jednoho poloplná a pro jiného poloprázdná.", Action = "Akce: Podebatuj s někým o svých prioritách, bez kterých se nedá žít.", IsLeft = true},
                new Card{ Heading = "6. Fáze učení mozku:", Text = "Proces osvojování si nového návyku má čtyři fáze:\n1.nevědomá a nezkušená\n – starý zvyk / automatismus\n2. vědomá, nezkušená\n – získá se po prvních 21 opakováních\n3. vědomá, zkušená\n – získá se po dalších 1 000 opakováních (fáze 1 021 opakování zabere přibližně 90 dnů)\n4.nevědomá, zkušená\n – nový návyk / automatismus", Action = "Akce: Když se ti něco hned nepovede, nenadávej si za to. Naopak si polož ruku na rameno a řekni si: „Žádný učený z nebe nespadl. Vytvářím nový návyk.“ A původní „To jsem ale de..l.“ nahraď „Někdy se to prostě nepovede, a co?“.", IsLeft = true},
                new Card{ Heading = "7. Každý mozek je jiný",Text =  "Každé vnímání světa je jiné.\n\nNikdo nemá tvé zkušenosti, tvé zvyky, tvé myšlenky, tvé zdroje. Nikdo ti nerozumí tak dokonale jako ty. Jsi jediný odborník na svůj život.\n\nŘešení všech svých dilemat znáš. Jen je vytáhnout z hlubin mysli na světlo.", Action = "Akce: Podívej se, v kolika zásadních otázkách se společnost dělí do různých názorových skupin.Najdeš jedno téma, v němž jsou všichni zajedno?", IsLeft = true},
                new Card{ Heading = "8. Zvyk je železná košile", Text = "I když je těžká, svléknout se dá.\n\nJe dobré mít někoho, kdo ti pomůže „sundat“ starý nežádoucí zvyk a pomůže ti „obléct“ nový žádoucí návyk, který ukotvíš ve svém životě.", Action = "Akce: Vypiš si(zlo)zvyky, kterých se chceš zbavit. Napiš si také návyky, které by stály za osvojení. Co všechno díky nim získáš?", IsLeft = true},
                new Card{ Heading = "9. Prioritizace", Text = "Prioritizuj podle míry důležitosti pro sebe.\n\nJe - li to, čeho chceš dosáhnout, pro tebe v danou chvíli opravdu důležité, dej tomu absolutní prioritu. Mozek snáze pochopí, že to myslíš vážně.\n\nNapříklad: Mám v úterý odpoledne sezení s koučem a do toho mi zavolá kamarád s nabídkou badmintonu. Svým výběrem dávám mozku jasně najevo, co je pro mě v danou chvíli důležité. Opakovanou volbou toto pochopitelně podpořím.", Action = "Akce: Co je tvou jasnou prioritou, která je nadřazená vždy a všemu? Co by mělo být tvou prioritou, a není?", IsLeft = true},
                new Card{ Heading =  "10. Nevyžádané rady – informační dieta", Text = "Nesmírně osvěžující informační dieta působí blahodárně na všech úrovních žití. Dopřej si pauzu od médií, sociálních sítí, telefonátů, reklam, schůzek. Buď jen ty, bez tlaku na to, co máš a nemáš dělat apod.\nPo takové informační dietě určitě pocítíš lehkost a svěžest.Tohle má pro tebe ještě jeden efekt.Naučíš se efektivněji žádat o radu, když to skutečně budeš potřebovat.", Action = "Akce: Jeden den přistupuj ke všem informacím, které na tebe automaticky míří, a nejsou tebou výslovně vyžádány(TV, rádio, podcasty, články, zpravodajství, sociální sítě atd.), jako k nevyžádaným radám a odmítni je, nekompromisně, slušně a důsledně.Nevyžádané rady se od tebe budou odrážet jako od zrcadla. Bude to pro tebe nesmírně osvobozující a očistný proces.", IsLeft = true},
                new Card{ Heading =  "11. Inventura úspěchů", Text = "Naše přání bereme jako nedosažitelný sen do chvíle, než si ho splníme. Pak již bagatelizujeme jeho náročnost. I to, kolik úsilí nás stála realizace. Rychle zapomínáme na trnitou cestu, která nás mnohé naučila. Místo toho, abychom se z ní dlouhodobě poučili, říkáme „tak strašné/ náročné to zase nebylo“.", Action = "Akce: Vypiš si své dosavadní úspěchy.Co považuješ za své životní úspěchy? Jaké konkrétní úsilí tě jejich dosažení stálo? Jaké zlomové okamžiky tě na cestě za nimi čekaly?", IsLeft = true},
                new Card{ Heading =  "12. Opuštění komfortní zóny",Text =  "Dělat věci nově, jinak, neobvykle.\nNapříklad: Zvolit jinou cestu do práce, vystoupit třeba o stanici dříve a zbytek dojít pěšky. Dát si ráno studenou sprchu. Nakoupit v jiném obchodě než obvykle. K obědu si dát něco dosud neochutnaného. Po příchodu z práce udělat doma jako první něco úplně jiného než obyčejně. Dát si výzvu týdne a splnit ji. Zkrátka udělej cokoliv, co vybočuje z tvého běžného chování. Otestuj své vlastní hranice, dogmata a tabu.", Action = "Akce: Na nějaký čas přestaň dělat nějakou věc, která je pro tebe pohodlná, nebo naopak udělej něco jinak, nově.Pro začátek postačí i drobné změny v zažitých denních rituálech. Zapiš si nebo sdílej, co „šíleného“ se ti povedlo udělat, jak to na tebe zapůsobilo a své pocity při tom a potom.", IsLeft = true},
                new Card{ Heading =  "13. Přání v posledním týdnu života", Text = "Také máš na všechno hromadu času? Vždyť to ještě vydrží nebo udělám to zítra, za týden, po dovolené. Není na co čekat na kdy odkládat.Nikdy nevíme, kolik nám ještě zbývá času.", Action = "Akce: Napiš na papír svá přání, která si chceš splnit, kdyby ti zbýval poslední týden života.Text nech pár dnů uležet a pak se k němu vrať.Vrať se k němu ještě jednou za několik týdnů.Velké překvapení tě nemine.", IsLeft = true},
                new Card{ Heading =  "14. Ne předponě ne",Text =  "Povšimni si, jak často začínají naše věty záporem. „Nepůjdeš ven?“ „Nemáš čas?“ „Nehodilo by se ti to?“ „Nedáš si něco dobrého na zub?“\n\nCo tak toho „NE“ trochu ubrat? Nejde v žádném případě o to, vyhnat slovo „ne“ ze svého života. To určitě ne.Ne je potřebné, stejně jako ano, když ho použiješ v pravý čas. Jde o zmírnění záporného postoje ke každodenním činnostem, který tak často vyjadřujeme větami uvozenými právě negací.", Action = "Akce: Neustále, vždy a všude, v práci i doma zkoušej odstraňovat negaci ze slovníku i myšlení.Soustřeď se a vědomě se opravuj.", IsLeft = true},
                new Card{ Heading =  "15. Pochvala vs. ocenění", Text = "Pravé ocenění je na rozdíl od neurčité pochvaly konkrétní. Takže vím přesně, za co druhého oceňuji. Mělo by být upřímné, tedy věřím tomu, co říkám. Ocenění klade důraz na úsilí toho, kdo je oceňován. Ocenění si všímá právě samotné tvorby, změny a vynaložené práce. Při oceňování upozadíme své ego, zásadně nehodnotíme. Ano, může se nám práce druhého líbit, ale ocenění je co nejobjektivnější, takže vlastní hodnotící soud potlačíme.\n\nNapříklad: Malý chlapec v lese postavil domeček pro skřítky. Nekonkrétní pochvala: „Ty jsi ale šikovný chlapeček.“ Konkrétní ocenění: „To ti muselo dát hodně práce. A kde jsi sehnal tu zelenou šišku? Vždyť tady jsou jen hnědé.“ Naučíš-li se oceňovat druhé, dokážeš pak ocenit i sebe. A neboj se, není to domýšlivost. Oceňuješ přece konkrétní práci a ta si to zaslouží.", Action = "Akce: Zkus jeden den místo nekonkrétní chvály a subjektivního hodnocení druhé oceňovat.", IsLeft = true},
                new Card{ Heading =  "16. Naše limity a hranice",Text =  "Veškeré naše jednání ovlivňují zažité vzorce chování, které současně vytváří hranice našeho myšlení a konání. Tyto limity se v nás vytváří od prenatálu. Neustále. Jsme doslova bombardováni zažitými omezujícími vzorci chování. A to prostřednictvím svých nejbližších, našeho okolí, členů sociálních skupin, do nichž patříme. Přitom tyto limity jsou jen uměle vytvořené hranice.", Action = "Akce: Vyber si nějaký limit, který tě denně obtěžuje, a poznamenej si ho. Pod něj si napiš, co ti přináší a v čem ti brání.Nad sloupec, co ti přináší, napiš znaménko „−“ a nad sloupec, v čem ti brání, napiš znaménko „+“. Ano, je to správně.Protože omezující limit ti přináší do života nepohodlí a nesoulad, kdežto to, v čem ti brání, je většinou právě to, co si přeješ, co chceš.", IsLeft = true},
                new Card{ Heading =  "17. Čas navíc", Text = "Jednoduchý způsob, jak nesklouznout do nežádoucího automatismu.\n\nHluboký nádech.\n\nKdyž jsou tvé emoce rychlejší než nádech, sklouzneš do automatismu. Nádech ti dá potřebný čas, aby se tak nestalo.", Action = "Akce: Zkoušej nádech při náhlém návalu emocí.", IsLeft = true},
                new Card{ Heading =  "18. Vesmírná objednávka", Text = "Udělej si konkrétní vesmírnou objednávku a celý vesmír se spojí, aby se tvé přání splnilo.", Action = "Akce: Napiš si vesmírnou objednávku. Buď konkrétní. Objednej si, co opravdu chceš, kolik toho má být a kdy ti to mají přivézt.", IsLeft = true},
                new Card{ Heading =  "19. Do roka a do dne",Text =  "Co se ti má splnit do roka a do dne?", Action = "Akce: Co chceš jinak? Kterými konkrétními chytrými kroky k tomu dojdeš?", IsLeft = true},
                new Card{ Heading =  "20. Chytré kroky", Text = "Všechno, co máš, je výsledkem tvých dlouhodobých CHYTRÝCH kroků.", Action = "Akce: Podívej se na svůj jakýkoliv úspěch. Není podstatné, jak moc je pro tebe významný. Projdi si své jednotlivé CHYTRÉ kroky, které k němu vedly.", IsLeft = true},
                new Card{ Heading =  "21. Porovnávání", Text = "Jabloň, nebo smrk? Kdo je lepší? Jabloň se směje smrku: vždyť ani nemáš sladké plody, jsi k ničemu. Smrk se směje jabloni: v zimě jsi nahá, nemáš ani šišku, dokonce ani nevoníš. Každý je jiný.Každý máme vlastní důvod ke své existenci.Porovnáváním přímo do „pekla“.", Action = "Akce: Pohlídej si porovnávání.Zejména u dětí - sourozenců je to hned po ruce.Děti jsou osobnosti, stejně jako my dospělí. Také je každé jiné.", IsLeft = true},
                new Card{ Heading =  "22. Rozděl se a nesuď",Text =  "Dej a bude ti dáno. Přej a bude ti přáno. Rozděl se o to, čeho máš málo. Ale nic za to nečekej. Dovol druhým, ať jsou sví, dovol jim dělat chyby. Dovol si je nesoudit.", Action = "Akce: Všímej si chyb druhých, ale nesuď je a neodsuzuj. Nemáš všechny zdroje(informace, zkušenosti, vstupy) druhých.", IsLeft = true},
                new Card{ Heading =  "23. Uvolnění prostoru", Text = "Pro lepší a klidnější přemýšlení, když se potřebuješ soustředit na své myšlenky(před jednáním, před zkouškou apod.), se hodí tzv. uvolnění prostoru.", Action = "Akce: Promítni si v hlavě situaci(bez podrobností!), zápornou, ale i pozitivní, která tě v daný okamžik nejvíce ovlivňuje(např.ranní srážka s blbcem, hádka s partnerem, sekec od šéfa nebo to, že se šíleně těšíš domů, protože jdeš se svou láskou do vaší oblíbené restaurace na večeři nebo tě čeká tvůj oblíbený sport). Uvědom si prosím, jaká to je emoce, pojmenuj ji jedním slovem a zjisti, kde na těle ji cítíš.Potom tuto emoci i s pomocí gesta odlož(kladnou si pak nezapomeň vzít zpět).", IsLeft = false},
                new Card{ Heading =  "24. Podporování",Text =  "Jak by vypadal tvůj život, kdyby tě tví nejbližší podporovali, věřili v tebe, ve tvé schopnosti? Kdyby ti fandili? Naruš začarovaný kruh zajetých kolejí neposkytování podpory v životních snech. Ty můžeš podporovat, věřit a fandit svým dětem, blízkým, přátelům a nezapomeň i na sebe.", Action = "Akce: Podporuj své blízké. Potřebují to. Navíc se ti tato energie bohatě vrátí.Podporujte se navzájem.Klidně začněte malými věcmi.", IsLeft = false},
                new Card{ Heading =  "25. Myslet jako bohatý", Text = "Žít s pocitem „MÁM NA TO“ nebo „bohužel, to si nemohu dovolit“.\n\nŽít s pocitem „MÁM NA TO“ je základem myšlení bohatého.", Action = "Akce: Dej si do peněženky 3 tisíce. Líbí se ti věc za 2 590 Kč.Stačí jít do obchodu, víš, že na to máš. Ovšem nekupuj ji.Důležité je, že MŮŽEŠ.", IsLeft = false},
                new Card{ Heading =  "26. Kde je pozornost, je energie", Text = "Princip se zaměřením pozornosti funguje vždy, všude a na vše.\nNapříklad: Nesu několik krabic, hrozí, že spadnou. Zaměřím pozornost na to, že mohou spadnout. Co myslíš, že se stane? Ano, spadnou.Je mi zima na ruce.Zaměřím na ně pozornost s tím, že je mi teplo. Ruce se skutečně zahřejí.", Action = "Akce: Tři dlaně o sebe, až v nich pocítíš horko. Dej je pár centimetrů od sebe. Představ si, že v dlaních držíš kouli o velikosti grepu, a koulej ji. Potom přibližuj dlaně pomalinku k sobě, jako bys kouli stlačoval.Pocítíš jemný odpor energie.", IsLeft = false},
                new Card{ Heading =  "27. Klíčem k mozku jsou emoce", Text = "Emoce nám sdělují, v jakém souladu jsme sami se sebou. Zjednodušeně řečeno, emoce jsou kontrolním panelem, na kterém je škála od „je to hodně zlé“ přes „nejsem OK“ až po „jsem OK“ a „je to super“.\n\nI sebelogičtější mozek se nakonec rozhoduje na základě emocí. Následnou analýzou pak většinou pouze zdůvodňujeme svá dřívější emocionální rozhodnutí.", Action = "Akce: Všímej si svých rozhodnutí a poctivě si přiznej, co i po důkladné úvaze nakonec převážilo jazýček vah.", IsLeft = false},
                new Card{ Heading =  "28. Pocity nelžou", Text = "Pocity nikdy nelžou, myšlenky mohou.\n\nNapříklad: Že se mnou někdo manipuluje, poznám mnohem rychleji pocitově – necítím se v tom dobře. Rozumem manipulaci odhalím většinou až ex post, což může být pozdě.", Action = "Akce: Kdy se tvůj pocit osvědčil?", IsLeft = false},
                new Card{ Heading =  "29. Vision board", Text = "Vision board neboli nástěnka snů je velkým pomocníkem pro zaměření se na svá přání. Sny se stávají cíli a ještě více se přiblíží.", Action = "Akce: Vytvoř si vision board se vším, čeho chceš dosáhnout například v následujícím roce. Vyfoť si ho a dej si ho jako tapetu do mobilu. Síla vizualizace se zmnohonásobí.", IsLeft = false},
                new Card{ Heading =  "30. Nálady jsou mé", Text = "Mé nálady jsou mé a nikoho jiného. Vyvěrají z mého nitra a to je také mé. Jakýkoliv vnější stimul je vždy poměřován mou optikou se všemi mými zkušenostmi, prožitky, vzorci chování, teprve pak jej vnímám. Jak s vnějším podnětem naložím, je jen na mém uvážení, nikdo jiný to za mě neudělá. Svět je takový, jaký ho vnímám. Nemůže být ničím víc ani míň. Jen já ho můžu vnímat tak či onak. A pak bude takový či onaký.", Action = "Akce: Říkej si každý den několikrát jako mantru: „Mé nálady jsou mé a nikoho jiného. Jak naložím s vnějšími podněty, je jen na mně.“ Čiň tak, dokud si to dokonale nezvědomíš.", IsLeft = false},
                new Card{ Heading =  "31. Velký úspěch",Text =  "", Action = "Akce: Vzpomeň si na svůj velký úspěch. Byla to paráda, co? Tento vítězný pocit si vědomě „nakopíruj“ do každé další budoucí akce.", IsLeft = false},
                new Card{ Heading =  "32. Strach ze selhání", Text = "Proč je v nás strach ze selhání tak hluboce zakořeněn? Protože v minulosti selhání znamenalo smrt. Mozek bohužel neumí intenzitu selhání vyhodnotit. Vyplatí se proto použít staré osvědčené: Zastav se a napočítej do deseti. První nával paniky odezní a přiměješ tak mozek přemýšlet.", Action = "Akce: Dej mozku čas, aby při chybě zjistil, že opravdu, ale opravdu nejde o život.", IsLeft = false},
                new Card{ Heading =  "33. Strach v životě", Text = "Strach v životě je jako sůl v polévce. Neosolená ani přesolená polévka se nedá jíst.", Action = "Akce: Pojmenuj jeden svůj strach. Představ si ho jako nějakou bytost, například pohádkovou, a nakresli ji. Už teď je strach poloviční. Bubák vytažený zpod postele na světlo není moc děsivý, co?", IsLeft = false},
                new Card{ Heading =  "34. Změna znaménka", Text = "Veškeré energii, která k tobě přichází, určuješ znaménko ty. Křičíš na mě? Děkuji ti, věnuješ mi pozornost, a tedy i energii.", Action = "Akce: Začni nejprve něčím snadným. Například televizními zprávami nebo rozhovorem o ničem s nějakým věčným pesimistou či prudičem. Nastav si vědomě, že veškerá energie, která k tobě proudí, je ti prospěšná, užitečná. Pak měň polaritu energie k tobě vysílané po částech (například po jednotlivých myšlenkách, větách). Z minus udělej plus. Usmívej se, to pomáhá. Změnu znaménka trénuj kdekoliv a kdykoliv. Klidně to doprovázej i slovy, gesty, čímkoliv, co ti pomůže si tuto techniku zafixovat.", IsLeft = false},
                new Card{ Heading =  "35. Tvá vášeň", Text = "Ve vášni se spojují a projevují tvé silné stránky. Na tom se dá stavět. Vyplatí se věnovat pozornost vlastní vášni.", Action = "Akce: Definuj svou vášeň. O čem dokážeš mluvit hodiny a hodiny s plamínky v očích? Co ti tvá vášeň může všechno dát? Jak ti může změnit život? Definuj konkrétně a v bodech.", IsLeft = false},
                new Card{ Heading =  "36. Konflikt", Text = "Konflikt je jen jiný úhel pohledu.", Action = "Akce: Při hrozícím konfliktu se podívej na řešené téma myšlenkami druhého diskutujícího. Půjč si na chvíli co nejvíce jeho zdrojů (informace, zkušenosti, záměr). Pomůže ti to alespoň z části pochopit jeho jednání a ve většině případů konflikt zažehnat ještě před jeho vypuknutím.", IsLeft = false},
                new Card{ Heading =  "37. Mladý a starý duch",Text =  "Duchem mladý člověk zvrátí cokoliv. Duchem starý strčí hlavu do písku a nechá si nakopat zadek.", Action = "Akce: Dělej to, co tě omlazuje.", IsLeft = false},
                new Card{ Heading =  "38. Není pozdě", Text = "Čím odlišíš dnešní den od včerejšího a zítřejšího? Nikdy není pozdě na vyzkoušení něčeho nového.", Action = "Akce: Každý den udělej nebo se nauč něco nového.", IsLeft = false},
                new Card{ Heading =  "39. Lhaní do kapsy", Text = "Když si lžeš do kapsy, počítej s tím, že každá kapsa má omezenou nosnost. A lež dokáže být někdy pěkně těžká.", Action = "Akce: Řekni si pár rčení o lži a pravdě. Zamysli se nad jejich skutečným sdělením. Nejsou to taková klišé, jak to na první pohled může vypadat, že?", IsLeft = false},
                new Card{ Heading =  "40. Nepružnost vůči změnám", Text = "Nepružnost vůči změnám nás činí náchylnějšími vůči strachům. Změna je výzva, ne protivník.", Action = "Akce: Protahuj své tělo. Je prokázáno, že v zatuhlém těle těžko hledat flexibilního ducha.", IsLeft = false},
                new Card{ Heading =  "41. Jedinečnost",Text =  "Jedinečnost není o tom, být nej. Tvá jedinečnost je tvá originalita, kterou druzí hledají. Pěstuj a hýčkej svou odlišnost (podivnost).", Action = "Akce: V čem spočívá tvoje jedinečnost? Co je tvé, co je ti opravdu vlastní? Co je tvoje „podivínství“?", IsLeft = false},
                new Card{ Heading =  "42. Láska, víra, naděje", Text = "LÁSKA je základní báze života.\n\nVÍRA je mocná hybná síla.\n\nNADĚJE je kompas cesty, která tě dovede.", Action = "Akce: Dovol si být láskou, cítit lásku, dávat lásku, přijímat lásku. Temenem hlavy nadechuj vesmírnou moudrost. Srdcem vydechuj světelný proud lásky.", IsLeft = false},
                new Card{ Heading =  "43. Světlo", Text = "Světlo jsi a ve světlo se obrátíš. Opravdu se potřebuješ zatemnit?", Action = "Akce: Zapal si svíčku. Koukej se do plamene. Myšlenky si představ jako suché podzimní listí, nechej je shořet v plamenu svíčky. Nad plamenem si zahřej dlaně a tímto očistným teplem si omyj obličej a hlavu.", IsLeft = false},
                new Card{ Heading =  "44. Křídla",Text =  "Ztracenou svobodu si vezmi zpět. Přímo teď ti na tvých zádech rostou křídla.", Action = "Akce: S každým nádechem a výdechem nabývají na své síle. Jsi volnost sama.", IsLeft = false},
                new Card{ Heading =  "45. Partnerství pro život",Text =  "Hledáš lásku, se kterou zestárneš? Co takhle najít lásku, se kterou nestárneš, ale naopak mládneš? Buďte oba stále bláznivě zamilovaní!", Action = "Akce: Udělejte společně bláznivou věc, o které ostatní říkají, že už se k vašemu věku nehodí. Třeba si postavte stan v obýváku.", IsLeft = false},
                new Card{ Heading =  "46. Čas na změnu", Text = "Nečekej na nic, využij toho, co máš teď. Udělej to, co chceš udělat už dlouho.\nPřijmi ten dar dnešního dne a začni hned. Ne pak, ale teď! Dnes. Bez výmluv. Udělej to jinak, než doposud.", Action = "Akce: Co konkrétního můžeš udělat pro sebe dnes, aby se z toho dalo těžit zítra?", IsLeft = false},
                new Card{ Heading =  "47. Vzestup a pád",Text =  "Máš pocit, že jsi na dně? Tak se ode dna odraz a stoupej výš, až nad počáteční bod tvého klesání.\nNapříklad: Dítě, které se učí chodit, v průměru 2 000krát upadne. Jsou pro něj pády důvodem, aby se nenaučilo chodit?", Action = "Akce: Všechno zlé je pro něco dobré. Jakým způsobem tě posílil poslední pád?", IsLeft = false},
                new Card{ Heading =  "48. Vůně", Text = "Rozpomeň se.", Action = "Akce: Vybav si 3 vůně z dětství.", IsLeft = false},
                new Card{ Heading =  "49. Jídla", Text = "Rozpomeň se.", Action = "Akce: Vybav si tři jídla z dětství, která z tvého jídelníčku zmizela.", IsLeft = false}
            };

        static async Task Init()
        {
            if (db == null)
            {
                //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Card>();
                LoadData();
            }
        }

        private static async Task LoadData()
        {
            foreach (var card in cards)
            {
                await AddCard(card.Heading, card.Text, card.Action, card.IsLeft);
            }
        }

        public static async Task AddCard(string heading, string text, string action, bool isLeft)
        {
            await Init();

            var card = new Card
            {
                //ID = id,
                Heading = heading,
                Text = text,
                Action = action,
                IsLeft = isLeft
            };

            var rowsAffected = await db.InsertAsync(card);
        }

        public static async Task RemoveCard(int id)
        {
            await Init();

            await db.DeleteAsync<Card>(id);
        }

        public static async Task<IEnumerable<Card>> GetCard()
        {
            await Init();

            var Card = await db.Table<Card>().ToListAsync();
            return Card;
        }

        //public async Task<bool> AddAsync(Card item)
        //{
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> UpdateAsync(Card item)
        //{
        //    var oldItem = items.Where((Card arg) => arg.ID == item.ID).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var oldItem = items.Where((Card arg) => arg.ID == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<Card> GetAsync(int id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        //}

        //public async Task<IEnumerable<Card>> GetAllAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}
    }
}