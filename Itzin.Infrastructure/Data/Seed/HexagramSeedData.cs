using Itzin.Core.Entities;

namespace Itzin.Infrastructure.Data.Seed;

public static class HexagramSeedData
{
    public static List<Hexagram> GetHexagrams()
    {
        return new List<Hexagram>
        {
            // Hexagram 1: The Creative
            new Hexagram
            {
                Id = 1,
                Number = 1,
                ChineseName = "乾",
                Pinyin = "Qián",
                EnglishName = "The Creative",
                RussianName = "Творчество",
                Binary = "111111",
                Unicode = "☰",
                UpperTrigram = 1,
                LowerTrigram = 1,
                JudgmentEn = "THE CREATIVE works sublime success, Furthering through perseverance.",
                JudgmentRu = "ТВОРЧЕСТВО приносит возвышенный успех. Благоприятствует стойкость.",
                ImageEn = "The movement of heaven is full of power. Thus the superior man makes himself strong and untiring.",
                ImageRu = "Движение неба полно силы. Так благородный муж делает себя сильным и неутомимым.",
                Line1En = "Hidden dragon. Do not act.",
                Line1Ru = "Скрытый дракон. Не действуй.",
                Line2En = "Dragon appearing in the field. It furthers one to see the great man.",
                Line2Ru = "Дракон является на поле. Благоприятно увидеть великого человека.",
                Line3En = "All day long the superior man is creatively active. At nightfall his mind is still beset with cares. Danger. No blame.",
                Line3Ru = "Весь день благородный муж творчески активен. В сумерках его ум все еще полон забот. Опасность. Без вины.",
                Line4En = "Wavering flight over the depths. No blame.",
                Line4Ru = "Колеблющийся полет над глубинами. Без вины.",
                Line5En = "Flying dragon in the heavens. It furthers one to see the great man.",
                Line5Ru = "Летящий дракон в небесах. Благоприятно увидеть великого человека.",
                Line6En = "Arrogant dragon will have cause to repent.",
                Line6Ru = "Высокомерный дракон будет иметь причину раскаиваться."
            },
            
            // Hexagram 2: The Receptive
            new Hexagram
            {
                Id = 2,
                Number = 2,
                ChineseName = "坤",
                Pinyin = "Kūn",
                EnglishName = "The Receptive",
                RussianName = "Исполнение",
                Binary = "000000",
                Unicode = "☷",
                UpperTrigram = 2,
                LowerTrigram = 2,
                JudgmentEn = "THE RECEPTIVE brings about sublime success, Furthering through the perseverance of a mare. If the superior man undertakes something and tries to lead, He goes astray; But if he follows, he finds guidance. It is favorable to find friends in the west and south, To forego friends in the east and north. Quiet perseverance brings good fortune.",
                JudgmentRu = "ИСПОЛНЕНИЕ приносит возвышенный успех. Благоприятствует стойкость кобылы. Если благородный муж предпринимает что-то и пытается вести, он сбивается с пути; но если он следует, он находит руководство. Благоприятно найти друзей на западе и юге, отказаться от друзей на востоке и севере. Тихая стойкость приносит счастье.",
                ImageEn = "The earth's condition is receptive devotion. Thus the superior man who has breadth of character carries the outer world.",
                ImageRu = "Состояние земли - восприимчивая преданность. Так благородный муж, обладающий широтой характера, несет внешний мир.",
                Line1En = "When there is hoarfrost underfoot, Solid ice is not far off.",
                Line1Ru = "Когда под ногами иней, твердый лед недалеко.",
                Line2En = "Straight, square, great. Without purpose, Yet nothing remains unfurthered.",
                Line2Ru = "Прямой, квадратный, великий. Без цели, но ничто не остается без продвижения.",
                Line3En = "Hidden lines. One is able to remain persevering. If by chance you are in the service of a king, Seek not works, but bring to completion.",
                Line3Ru = "Скрытые линии. Можно оставаться стойким. Если случайно ты на службе у царя, не ищи дел, но доводи до завершения.",
                Line4En = "A tied-up sack. No blame, no praise.",
                Line4Ru = "Завязанный мешок. Без вины, без похвалы.",
                Line5En = "A yellow lower garment brings supreme good fortune.",
                Line5Ru = "Желтая нижняя одежда приносит высшее счастье.",
                Line6En = "Dragons fight in the meadow. Their blood is black and yellow.",
                Line6Ru = "Драконы сражаются на лугу. Их кровь черная и желтая."
            },

            // Hexagram 3: Difficulty at the Beginning
            new Hexagram
            {
                Id = 3,
                Number = 3,
                ChineseName = "屯",
                Pinyin = "Zhūn",
                EnglishName = "Difficulty at the Beginning",
                RussianName = "Начальная трудность",
                Binary = "010001",
                Unicode = "☵",
                UpperTrigram = 3,
                LowerTrigram = 4,
                JudgmentEn = "DIFFICULTY AT THE BEGINNING works supreme success, Furthering through perseverance. Nothing should be undertaken. It furthers one to appoint helpers.",
                JudgmentRu = "НАЧАЛЬНАЯ ТРУДНОСТЬ приносит высший успех. Благоприятствует стойкость. Ничего не следует предпринимать. Благоприятно назначить помощников.",
                ImageEn = "Clouds and thunder: The image of DIFFICULTY AT THE BEGINNING. Thus the superior man Brings order out of confusion.",
                ImageRu = "Облака и гром: образ НАЧАЛЬНОЙ ТРУДНОСТИ. Так благородный муж создает порядок из хаоса.",
                Line1En = "Hesitation and hindrance. It furthers one to remain persevering. It furthers one to appoint helpers.",
                Line1Ru = "Колебание и препятствие. Благоприятно оставаться стойким. Благоприятно назначить помощников.",
                Line2En = "Difficulties pile up. Horse and wagon part. He is not a robber; He wants to woo when the time comes. The maiden is chaste, She does not pledge herself. Ten years--then she pledges herself.",
                Line2Ru = "Трудности накапливаются. Лошадь и повозка расстаются. Он не разбойник; он хочет ухаживать, когда придет время. Дева целомудренна, она не дает обещаний. Десять лет - и тогда она обязуется.",
                Line3En = "Whoever hunts deer without the forester Only loses his way in the forest. The superior man understands the signs of the time And prefers to desist. To go on brings humiliation.",
                Line3Ru = "Кто охотится на оленя без лесничего, только заблудится в лесу. Благородный муж понимает знаки времени и предпочитает воздержаться. Продолжать - значит навлечь унижение.",
                Line4En = "Horse and wagon part. Strive for union. To go brings good fortune. Everything acts to further.",
                Line4Ru = "Лошадь и повозка расстаются. Стремись к союзу. Идти - приносит счастье. Все способствует.",
                Line5En = "Difficulties in blessing. A little perseverance brings good fortune. Great perseverance brings misfortune.",
                Line5Ru = "Трудности в благословении. Немного стойкости приносит счастье. Великая стойкость приносит несчастье.",
                Line6En = "Horse and wagon part. Bloody tears flow.",
                Line6Ru = "Лошадь и повозка расстаются. Текут кровавые слезы."
            },

            // Hexagram 4: Youthful Folly
            new Hexagram
            {
                Id = 4,
                Number = 4,
                ChineseName = "蒙",
                Pinyin = "Méng",
                EnglishName = "Youthful Folly",
                RussianName = "Юношеское безрассудство",
                Binary = "100010",
                Unicode = "☶",
                UpperTrigram = 4,
                LowerTrigram = 3,
                JudgmentEn = "YOUTHFUL FOLLY has success. It is not I who seek the young fool; The young fool seeks me. At the first oracle I inform him. If he asks two or three times, it is importunity. If he importunes, I give him no information. Perseverance furthers.",
                JudgmentRu = "ЮНОШЕСКОЕ БЕЗРАССУДСТВО имеет успех. Не я ищу молодого глупца; молодой глупец ищет меня. При первом оракуле я информирую его. Если он спрашивает два или три раза, это назойливость. Если он назойлив, я не даю ему информации. Стойкость благоприятствует.",
                ImageEn = "A spring wells up at the foot of the mountain: The image of YOUTH. Thus the superior man fosters his character By thoroughness in all that he does.",
                ImageRu = "Источник бьет у подножия горы: образ ЮНОСТИ. Так благородный муж воспитывает свой характер тщательностью во всем, что он делает.",
                Line1En = "To make a fool develop It furthers one to apply discipline. The fetters should be removed. To go on in this way brings humiliation.",
                Line1Ru = "Чтобы развить глупца, благоприятно применить дисциплину. Оковы должны быть сняты. Продолжать таким образом приносит унижение.",
                Line2En = "To bear with fools in kindliness brings good fortune. To know how to take women Brings good fortune. The son is capable of taking charge of the household.",
                Line2Ru = "Терпеть глупцов с добротой приносит счастье. Знать, как обращаться с женщинами, приносит счастье. Сын способен взять на себя управление домом.",
                Line3En = "Take not a maiden who, when she sees a man of bronze, Loses possession of herself. Nothing furthers.",
                Line3Ru = "Не бери девушку, которая, увидев человека из бронзы, теряет самообладание. Ничто не благоприятствует.",
                Line4En = "Entangled folly brings humiliation.",
                Line4Ru = "Запутанное безрассудство приносит унижение.",
                Line5En = "Childlike folly brings good fortune.",
                Line5Ru = "Детское безрассудство приносит счастье.",
                Line6En = "In punishing folly It does not further one To commit transgressions. The only thing that furthers Is to prevent transgressions.",
                Line6Ru = "Наказывая безрассудство, не стоит совершать проступки. Единственное, что благоприятствует, - это предотвращать проступки."
            },

            // Hexagram 5: Waiting
            new Hexagram
            {
                Id = 5,
                Number = 5,
                ChineseName = "需",
                Pinyin = "Xū",
                EnglishName = "Waiting (Nourishment)",
                RussianName = "Ожидание",
                Binary = "010111",
                Unicode = "☵",
                UpperTrigram = 3,
                LowerTrigram = 1,
                JudgmentEn = "WAITING. If you are sincere, You have light and success. Perseverance brings good fortune. It furthers one to cross the great water.",
                JudgmentRu = "ОЖИДАНИЕ. Если ты искренен, у тебя есть свет и успех. Стойкость приносит счастье. Благоприятно пересечь великую воду.",
                ImageEn = "Clouds rise up to heaven: The image of WAITING. Thus the superior man eats and drinks, Is joyous and of good cheer.",
                ImageRu = "Облака поднимаются к небу: образ ОЖИДАНИЯ. Так благородный муж ест и пьет, радуется и в хорошем настроении.",
                Line1En = "Waiting in the meadow. It furthers one to abide in what endures. No blame.",
                Line1Ru = "Ожидание на лугу. Благоприятно пребывать в том, что длится. Без вины.",
                Line2En = "Waiting on the sand. There is some gossip. The end brings good fortune.",
                Line2Ru = "Ожидание на песке. Есть некоторые сплетни. Конец приносит счастье.",
                Line3En = "Waiting in the mud Brings about the arrival of the enemy.",
                Line3Ru = "Ожидание в грязи приводит к прибытию врага.",
                Line4En = "Waiting in blood. Get out of the pit.",
                Line4Ru = "Ожидание в крови. Выбирайся из ямы.",
                Line5En = "Waiting at meat and drink. Perseverance brings good fortune.",
                Line5Ru = "Ожидание за едой и питьем. Стойкость приносит счастье.",
                Line6En = "One falls into the pit. Three uninvited guests arrive. Honor them, and in the end there will be good fortune.",
                Line6Ru = "Падаешь в яму. Прибывают три непрошеных гостя. Почитай их, и в конце будет счастье."
            }
        };
    }

    public static Dictionary<string, int> GetHexagramMapping()
    {
        // Maps binary patterns to hexagram numbers
        var mapping = new Dictionary<string, int>();
        var hexagrams = GetHexagrams();
        
        foreach (var hex in hexagrams)
        {
            mapping[hex.Binary] = hex.Number;
        }
        
        return mapping;
    }
}
