using UnityEngine;
using System.Collections;
using System.Text;

public static class RandomString
{
    public static string SelectRandom(string[] arr)
    {
        return arr[Random.Range(0, arr.Length)];
    }

    // Random String
    static readonly string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string MakeABC(int length)
    {
        return Make(ABC, length);
    }
    public static string MakeRadioStation()
    {
        return $"{MakeABC(1)}B{MakeABC(1)}";
    }
    static readonly string ABC0123 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    static readonly string Ascii = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890+=-!@#$%^&*()_+[]{};':,./<>?`~";
    public static string MakeABC123(int length)
    {
        return Make(ABC, length);
    }
    public static string MakeASCIIWithLine(int length)
    {
        var chars = new char[length];
        for (int i = 0; i < chars.Length; i++) chars[i] = (char)Random.Range(32, 127);
        return new string(chars);
    }
    public static string GetRandomSubstring(this string str, int length)
    {
        if (length > str.Length) length = str.Length;
        int startIndex = Random.Range(0, str.Length - length + 1);
        return str.Substring(startIndex, length);
    }
    public static string MakeHanguel(int length)
    {
        string abc =
            "가갸거겨고교구규그기" +
            "나냐너녀노뇨누뉴느니" +
            "다댜더뎌도됴두듀드디" +
            "라랴러려로료루류르리" +
            "마먀머며모묘무뮤므미" +
            "바뱌버벼보뵤부뷰브비" +
            "사샤서셔소쇼수슈스시" +
            "아야어여오요우유으이" +
            "자쟈저져조죠주쥬즈지" +
            "차챠처쳐초쵸추츄츠치" +
            "카탸커켜코쿄쿠큐크키" +
            "타탸터텨토툐투튜트티" +
            "파퍄퍼펴포표푸퓨프피" +
            "하햐허혀호효후휴흐히";
        return Make(abc, length);
    }
    static string Make(string abc, int length)
    {
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += abc[Random.Range(0, abc.Length)];
        }
        return result;
    }


    // Sample
    static public string SampleName()
    {
        return sample_names.Sample();
    }
    static string[] sample_names =
{
        "FatSharkYes", "Aversion", "ScrubBusters", "Aegrethiann", "Armothig", "Bergunnes", "Cailluinda",
        "HopeDespair", "Ethical", "Club Camel", "ErCode", "Entropy", "Guzzelinare","Crocmain",
        "Keator", "Clickzz", "Trickyx", "Cavyx", "Suffre", "Easyfreez", "Divineovi", "Burnxx","Galerius",
        "Quinrat", "Vyevyer", "Willexzzz", "Blindsaploll", "Alsokeatorr", "Cazzettee", "Aritrosyo",
        "Bigmandingo", "Imcaprise", "Djarmagodx", "PewDiePiee", "CanalKondZilla", "	JustinBieber",
        "DudePerfect", "LikeNastyaVlog", "EdSheeran", "Marshmello", "Badabun", "HolaSoyGerman",
        "whinderssonnunes", "JuegaGerman","TaylorSwift", "elrubiusOMG", "KatyPerry", "FelipeNeto",
        "Fernanfloo", "Rihanna", "Blackpink", "Nikita", "Telefilms", "Gaane", "Comunica","Vegetta777",
        "Cocomelong", "Yuyak", "XXTenta", "Mamula", "jackseptic", "Attitugant","Dermenwulf",  "Maddesley",
        "Senganan", "Avigfergis", "Floucetiaz", "Meredian", "Sighearon", "Beathferton", "Gwennier",
        "Moirpaskar", "Sirondin", "Belinnifet", "Gwenward", "Muilleach", "Smentiri", "Brabitiger",
        "Harithet", "NowecHa", "Thiannabe", "Catgualdo", "Heruimous", "Okreotiu", "Tillicia",
        "Hougharado", "Paolandeck", "Uigermune", "Chashlimid", "Hwaithia", "Pertairen", "Vaubattasa",
        "Cridgerna", "Kealycricus", "Prapparic", "Wilwynela", "Derianth", "Leopollyn", "Sammenice",
        "Gweanad", "Abedrili", "Ociader", "Gwili", "Criresh","Ardevon","Weannor", "Biolton", "Brittan",
        "Dugalana", "Duhiron", "Emageni", "Filion", "Gilrain", "Ivonat", "Saregan", "Tisser", "Xytristh",
        "Gorlatus","Kirithet","Hechtgalana","Touthoin","Ouenitig","Touthoin","Catiachilin",
    };
    static readonly string vowels = "aeiou";
    static readonly string consonants = "abcdefghijklmnopqrstvwxyz";
    public static string GenerateName(int length)
    {
        var r = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            var letters = i % 2 == 0 ? consonants : vowels;
            var letter = letters.Sample();
            if (i == 0) letter = char.ToUpper(letter);
            r.Append(letter);
        }
        return r.ToString();
    }


    static public string SampleSlogan()
    {
        return sample_slogans.Sample();
    }
    static string[] sample_slogans =
    {
        "",
        "Impossible is nothing",
        "Think different",
        "Purely You",
        "Look inside",
        "Just do it.",
        "Everyone's invited",
        "Confidence in Motion",
        "The Spirit Of Money",
        "Passion For Money",
        "Just The Best",
        "Money is What We Do",
        "Pure Lust",
        "Money, What Else?",
        "Yada Yada",
        "Heads Up",
        "Know the Ropes",
        "Happy as a Clam",
        "Easy As Pie",
        "Drive Me Nuts",
        "On the Same Page",
        "Beating a Dead Horse",
        "Needle In a Haystack",
        "Curiosity Killed The Cat",
        "Back to Square One",
        "Every Cloud Has a Silver Lining",
        "Break The Ice",
        "Fish Out Of Water",
        "Jumping the Gun",
        "Don't Look a Gift Horse",
        "Under the Weather",
        "Wild Goose Chase",
        "Hit Below The Belt",
        "Go Out On a Limb",
        "Ugly Duckling",
        "Roll With the Punches",
        "Right Out of the Gate",
        "Ride Him, Cowboy!",
        "Man of Few Words",
        "My Cup of Tea",
        "Cry Over Spilt Milk",
        "A Piece of Cake",
        "Knuckle Down",
        "Give a Man a Fish",
        "Drawing a Blank",
        "Shot In the Dark",
        "Read 'Em and Weep",
        "Two Down, One to Go",
    };

    static public string SampleOperation()
    {
        return $"{sample_epicAdjectives.Sample()} {sample_epicNouns.Sample()}";
    }
    static string[] sample_epicAdjectives = {
"Abject", "Audacious", "Brave", "Brilliant", "Cataclysmic", "Conquering", "Dangerous", "Destructive",
"Energetic", "Enigmatic", "Epic", "Fearsome", "Ferocious", "Fierce", "Furious", "Glorious", "Grand", "Heroic",
"Indomitable", "Invincible", "Legendary", "Mighty", "Powerful", "Relentless", "Resolute", "Savage",
"Fearless", "Sinister", "Spectacular", "Stoic", "Sublime", "Triumphant", "Unbelievable", "Unbreakable",
"Unmatched", "Unparalleled", "Unprecedented", "Unrivaled", "Unstoppable", "Victorious", "Visionary",
"Vigilant", "Vigorous", "Fierce",  "Flawless", "Formidable",  "Grandiose", "Impenetrable", "Impressive",
"Incredible", "Indestructible", "Innovative", "Magnificent", "Masterful", "Mythical", "Noble",
"Powerful", "Profound", "Relentless", "Resolute", "Seamless", "Superior", "Indomitable", "Untarnished",
"Wrathful", "Zealous", "Infinite", "Incomparable", "Incorruptible", "Celestial", "Solar", "Valiant",
"Mystical", "Aerial", "Omnipotent", "Audacious", "Radiant", "Primeval", "Sagacious", "Cryptic",
"Endless", "Supernal", "Esoteric", "Keen", "Destructive", "Immaculate", "Potent", "Sacred", "Blazing",
"Veiled", "Tenacious", "Majestic", "Luminous", "Vampiric", "Resilient", "Ethereal", "Vengeful", "Soundless",
"Lofty", "Cosmic", "Stalwart", "Untamed", "Enchanted", "Relentless", "Magical", "Icy", "Velvet", "Immortal",
"Pristine", "Gracious", "Twinkling", "Incisive", "Absolute", "Dynamic", "Unyielding", "Unfaltering",
"Epochal", "Grandiose", "Mythic", "Heroic", "Monumental", "Titanic", "Colossal", "Immense","Stupendous",
"Herculean","Prodigious","Apocalyptic","Mythopoeic"
    };
    static string[] sample_epicNouns = {
"Revolution", "Judgment", "Twilight", "Virtue", "Sovereign", "Citadel", "Conquest", "Tiara",
"Euphoria", "Pledge", "Destiny", "Honor", "Glory", "Fusion", "Verity", "Baton", "Mandate",
"Triangle", "Coordinator", "Horizon", "Reality", "Spectrum", "Orient", "Crystal", "Turning Point",
"Energy", "Heartbeat", "Massacre", "Accident", "Exploration", "Wave", "Blitz", "Incendiary",
"Spark", "Infinity", "Pandora", "Fantasy", "Harvest", "Star", "Prominence", "Overload",
"Dust", "Titan", "Silverlight", "Heritage", "Serenity", "Asteroid", "Cosmos", "Eclipse",
"Orion", "Spector", "Tornado", "Ghost", "Crescendo", "Prestige", "Vortex", "Eternity", "Apocalypse",
"Aurora", "Avalanche", "Blizzard", "Bolt", "Brimstone", "Cataclysm", "Catastrophe", "Chaos",
"Cayon", "Nova", "Soulfire", "Twilight", "Vengeance", "Whirlwind", "Wrath", "Zenith",
"Gate", "Eden", "City", "Sky", "Arrow", "Abyss", "Angel", "Apex", "Avenger", "Beast", "Blaze", "Bliss",
"Blossom", "Bravery", "Calm", "Chaos", "Clash", "Cloak", "Constellation", "Crisis", "Crown", "Darkness",
"Dawn", "Death", "Destiny", "Destruction", "Dream", "Eagle", "Ecstasy", "Elegance", "Excellence", "Explosion",
"Fate", "Fire", "Fury", "God", "Grace", "Grandeur", "Guardian", "Harmony", "Hope", "Horror", "Hydra",
"Imagination", "Inspiration", "Iris", "Justice", "Kingdom", "Legend", "Lightning", "Longevity",
"Lust", "Majesty", "Mastery", "Mettle","Midnight", "Miracle", "Mystery", "Nature", "Night", "Oblivion",
"Opportunity", "Order", "Paradise", "Passion", "Peace", "Perfection", "Phoenix", "Pinnacle", "Power",
"Praise", "Pride", "Promise", "Prosperity", "Purgatory", "Purity", "Rage", "Rebirth", "Rebellion", "Resurrection",
"Rhapsody", "Ritual", "Rising", "Salvation", "Shadow", "Shelter", "Silence", "Solstice", "Sorrow", "Storm",
"Strength", "Sun", "Triumph", "Twilight", "Unending", "Unity", "Victory", "Vision", "War", "Wisdom", "Wonder",
"Wrath", "Xeno", "Yin", "Moon", "System", "Supernova", "Gravity", "Comet", "Meteor", "Nebula",
"Orbit", "Galaxy", "Universe", "Planet", "Constellation","Ursa", "Draco", "Cygnus", "Cassiopeia","Leo",
"Gemini", "Pisces","Taurus","Aries","Cancer","Virgo","Libra","Scorpius","Sagittarius","Capricornus",
"Aquarius","Pegasus","Andromeda"
    };
}
