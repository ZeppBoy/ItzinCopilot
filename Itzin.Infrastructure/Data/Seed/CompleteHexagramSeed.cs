using Itzin.Core.Entities;

namespace Itzin.Infrastructure.Data.Seed;

public static class CompleteHexagramSeed
{
    public static List<Hexagram> GetAllHexagrams()
    {
        return new List<Hexagram>
        {
            CreateHexagram(1, "乾", "Qián", "The Creative", "Творчество", "111111", "䷀", 1, 1,
                "THE CREATIVE works sublime success, Furthering through perseverance.",
                "ТВОРЧЕСТВО приносит возвышенный успех. Благоприятствует стойкость.",
                "The movement of heaven is full of power. Thus the superior man makes himself strong and untiring.",
                "Движение неба полно силы. Так благородный муж делает себя сильным и неутомимым."),
            
            CreateHexagram(2, "坤", "Kūn", "The Receptive", "Исполнение", "000000", "䷁", 2, 2,
                "THE RECEPTIVE brings about sublime success, Furthering through the perseverance of a mare.",
                "ИСПОЛНЕНИЕ приносит возвышенный успех. Благоприятствует стойкость кобылы.",
                "The earth's condition is receptive devotion. Thus the superior man who has breadth of character carries the outer world.",
                "Состояние земли - восприимчивая преданность. Так благородный муж несет внешний мир."),
            
            CreateHexagram(3, "屯", "Zhūn", "Difficulty at the Beginning", "Начальная трудность", "010001", "䷂", 3, 4,
                "DIFFICULTY AT THE BEGINNING works supreme success, Furthering through perseverance.",
                "НАЧАЛЬНАЯ ТРУДНОСТЬ приносит высший успех. Благоприятствует стойкость.",
                "Clouds and thunder: The image of DIFFICULTY AT THE BEGINNING.",
                "Облака и гром: образ НАЧАЛЬНОЙ ТРУДНОСТИ."),
            
            CreateHexagram(4, "蒙", "Méng", "Youthful Folly", "Юношеское безрассудство", "100010", "䷃", 4, 3,
                "YOUTHFUL FOLLY has success. It is not I who seek the young fool; The young fool seeks me.",
                "ЮНОШЕСКОЕ БЕЗРАССУДСТВО имеет успех. Не я ищу молодого глупца; молодой глупец ищет меня.",
                "A spring wells up at the foot of the mountain: The image of YOUTH.",
                "Источник бьет у подножия горы: образ ЮНОСТИ."),
            
            CreateHexagram(5, "需", "Xū", "Waiting", "Ожидание", "010111", "䷄", 3, 1,
                "WAITING. If you are sincere, You have light and success. Perseverance brings good fortune.",
                "ОЖИДАНИЕ. Если ты искренен, у тебя есть свет и успех. Стойкость приносит счастье.",
                "Clouds rise up to heaven: The image of WAITING.",
                "Облака поднимаются к небу: образ ОЖИДАНИЯ."),
            
            CreateHexagram(6, "訟", "Sòng", "Conflict", "Конфликт", "111010", "䷅", 1, 3,
                "CONFLICT. You are sincere And are being obstructed. A cautious halt halfway brings good fortune.",
                "КОНФЛИКТ. Ты искренен и встречаешь препятствия. Осторожная остановка на полпути приносит счастье.",
                "Heaven and water go their opposite ways: The image of CONFLICT.",
                "Небо и вода идут противоположными путями: образ КОНФЛИКТА."),
            
            CreateHexagram(7, "師", "Shī", "The Army", "Войско", "000010", "䷆", 2, 3,
                "THE ARMY. The army needs perseverance And a strong man. Good fortune without blame.",
                "ВОЙСКО. Войску нужна стойкость и сильный человек. Счастье без вины.",
                "In the middle of the earth is water: The image of THE ARMY.",
                "В середине земли вода: образ ВОЙСКА."),
            
            CreateHexagram(8, "比", "Bǐ", "Holding Together", "Единение", "010000", "䷇", 3, 2,
                "HOLDING TOGETHER brings good fortune. Inquire of the oracle once again.",
                "ЕДИНЕНИЕ приносит счастье. Вопроси оракула еще раз.",
                "On the earth is water: The image of HOLDING TOGETHER.",
                "На земле вода: образ ЕДИНЕНИЯ."),
            
            CreateHexagram(9, "小畜", "Xiǎo Chù", "Small Taming", "Малое накопление", "110111", "䷈", 5, 1,
                "THE TAMING POWER OF THE SMALL Has success. Dense clouds, no rain from our western region.",
                "СИЛА МАЛОГО НАКОПЛЕНИЯ имеет успех. Густые облака, но нет дождя из нашего западного региона.",
                "The wind drives across heaven: The image of THE TAMING POWER OF THE SMALL.",
                "Ветер гонит по небу: образ СИЛЫ МАЛОГО НАКОПЛЕНИЯ."),
            
            CreateHexagram(10, "履", "Lǚ", "Treading", "Наступление", "111011", "䷉", 1, 6,
                "TREADING. Treading upon the tail of the tiger. It does not bite man. Success.",
                "НАСТУПЛЕНИЕ. Наступая на хвост тигра. Он не кусает человека. Успех.",
                "Heaven above, the lake below: The image of TREADING.",
                "Небо вверху, озеро внизу: образ НАСТУПЛЕНИЯ."),
            
            CreateHexagram(11, "泰", "Tài", "Peace", "Мир", "000111", "䷊", 2, 1,
                "PEACE. The small departs, The great approaches. Good fortune. Success.",
                "МИР. Малое уходит, великое приближается. Счастье. Успех.",
                "Heaven and earth unite: The image of PEACE.",
                "Небо и земля соединяются: образ МИРА."),
            
            CreateHexagram(12, "否", "Pǐ", "Standstill", "Упадок", "111000", "䷋", 1, 2,
                "STANDSTILL. Evil people do not further The perseverance of the superior man.",
                "УПАДОК. Злые люди не способствуют стойкости благородного мужа.",
                "Heaven and earth do not unite: The image of STANDSTILL.",
                "Небо и земля не соединяются: образ УПАДКА."),
            
            CreateHexagram(13, "同人", "Tóng Rén", "Fellowship", "Единомышленники", "111101", "䷌", 1, 5,
                "FELLOWSHIP WITH MEN in the open. Success. It furthers one to cross the great water.",
                "БРАТСТВО С ЛЮДЬМИ на открытом месте. Успех. Благоприятно пересечь великую воду.",
                "Heaven together with fire: The image of FELLOWSHIP WITH MEN.",
                "Небо вместе с огнем: образ БРАТСТВА С ЛЮДЬМИ."),
            
            CreateHexagram(14, "大有", "Dà Yǒu", "Great Possession", "Великое владение", "101111", "䷍", 5, 1,
                "POSSESSION IN GREAT MEASURE. Supreme success.",
                "ВЛАДЕНИЕ В ВЕЛИКОЙ МЕРЕ. Высший успех.",
                "Fire in heaven above: The image of POSSESSION IN GREAT MEASURE.",
                "Огонь на небе вверху: образ ВЛАДЕНИЯ В ВЕЛИКОЙ МЕРЕ."),
            
            CreateHexagram(15, "謙", "Qiān", "Modesty", "Скромность", "000100", "䷎", 2, 4,
                "MODESTY creates success. The superior man carries things through.",
                "СКРОМНОСТЬ создает успех. Благородный муж доводит дела до конца.",
                "Within the earth, a mountain: The image of MODESTY.",
                "Внутри земли гора: образ СКРОМНОСТИ."),
            
            CreateHexagram(16, "豫", "Yù", "Enthusiasm", "Воодушевление", "001000", "䷏", 4, 2,
                "ENTHUSIASM. It furthers one to install helpers And to set armies marching.",
                "ВООДУШЕВЛЕНИЕ. Благоприятно назначить помощников и послать войска в поход.",
                "Thunder comes resounding out of the earth: The image of ENTHUSIASM.",
                "Гром раздается из земли: образ ВООДУШЕВЛЕНИЯ."),
            
            CreateHexagram(17, "隨", "Suí", "Following", "Следование", "011001", "䷐", 6, 4,
                "FOLLOWING has supreme success. Perseverance furthers. No blame.",
                "СЛЕДОВАНИЕ имеет высший успех. Стойкость благоприятствует. Без вины.",
                "Thunder in the middle of the lake: The image of FOLLOWING.",
                "Гром в середине озера: образ СЛЕДОВАНИЯ."),
            
            CreateHexagram(18, "蠱", "Gǔ", "Work on the Decayed", "Исправление порчи", "100011", "䷑", 4, 5,
                "WORK ON WHAT HAS BEEN SPOILED Has supreme success. It furthers one to cross the great water.",
                "РАБОТА НАД ТЕМ, ЧТО ИСПОРЧЕНО, имеет высший успех. Благоприятно пересечь великую воду.",
                "The wind blows low on the mountain: The image of DECAY.",
                "Ветер дует внизу горы: образ ПОРЧИ."),
            
            CreateHexagram(19, "臨", "Lín", "Approach", "Приближение", "000011", "䷒", 2, 6,
                "APPROACH has supreme success. Perseverance furthers.",
                "ПРИБЛИЖЕНИЕ имеет высший успех. Стойкость благоприятствует.",
                "The earth above the lake: The image of APPROACH.",
                "Земля над озером: образ ПРИБЛИЖЕНИЯ."),
            
            CreateHexagram(20, "觀", "Guān", "Contemplation", "Созерцание", "110000", "䷓", 5, 2,
                "CONTEMPLATION. The ablution has been made, But not yet the offering.",
                "СОЗЕРЦАНИЕ. Омовение совершено, но еще не принесена жертва.",
                "The wind blows over the earth: The image of CONTEMPLATION.",
                "Ветер дует над землей: образ СОЗЕРЦАНИЯ."),
            
            CreateHexagram(21, "噬嗑", "Shì Kè", "Biting Through", "Стиснутые зубы", "101001", "䷔", 5, 4,
                "BITING THROUGH has success. It is favorable to let justice be administered.",
                "СТИСНУТЫЕ ЗУБЫ имеет успех. Благоприятно позволить правосудию свершиться.",
                "Thunder and lightning: The image of BITING THROUGH.",
                "Гром и молния: образ СТИСНУТЫХ ЗУБОВ."),
            
            CreateHexagram(22, "賁", "Bì", "Grace", "Убранство", "100101", "䷕", 4, 5,
                "GRACE has success. In small matters It is favorable to undertake something.",
                "УБРАНСТВО имеет успех. В малых делах благоприятно что-то предпринять.",
                "Fire at the foot of the mountain: The image of GRACE.",
                "Огонь у подножия горы: образ УБРАНСТВА."),
            
            CreateHexagram(23, "剝", "Bō", "Splitting Apart", "Разрушение", "100000", "䷖", 4, 2,
                "SPLITTING APART. It does not further one To go anywhere.",
                "РАЗРУШЕНИЕ. Не благоприятно идти куда-либо.",
                "The mountain rests on the earth: The image of SPLITTING APART.",
                "Гора покоится на земле: образ РАЗРУШЕНИЯ."),
            
            CreateHexagram(24, "復", "Fù", "Return", "Возврат", "000001", "䷗", 2, 4,
                "RETURN. Success. Going out and coming in without error.",
                "ВОЗВРАТ. Успех. Уход и возвращение без ошибки.",
                "Thunder within the earth: The image of THE TURNING POINT.",
                "Гром внутри земли: образ ПОВОРОТНОГО ПУНКТА."),
            
            CreateHexagram(25, "無妄", "Wú Wàng", "Innocence", "Беспорочность", "111001", "䷘", 1, 4,
                "INNOCENCE. Supreme success. Perseverance furthers.",
                "БЕСПОРОЧНОСТЬ. Высший успех. Стойкость благоприятствует.",
                "Under heaven thunder rolls: The image of INNOCENCE.",
                "Под небом гремит гром: образ БЕСПОРОЧНОСТИ."),
            
            CreateHexagram(26, "大畜", "Dà Chù", "Great Taming", "Великое накопление", "100111", "䷙", 4, 1,
                "THE TAMING POWER OF THE GREAT. Perseverance furthers.",
                "СИЛА ВЕЛИКОГО НАКОПЛЕНИЯ. Стойкость благоприятствует.",
                "Heaven within the mountain: The image of THE TAMING POWER OF THE GREAT.",
                "Небо внутри горы: образ СИЛЫ ВЕЛИКОГО НАКОПЛЕНИЯ."),
            
            CreateHexagram(27, "頤", "Yí", "Nourishment", "Питание", "100001", "䷚", 4, 4,
                "THE CORNERS OF THE MOUTH. Perseverance brings good fortune.",
                "УГЛЫ РТА. Стойкость приносит счастье.",
                "At the foot of the mountain, thunder: The image of PROVIDING NOURISHMENT.",
                "У подножия горы гром: образ ПРЕДОСТАВЛЕНИЯ ПИТАНИЯ."),
            
            CreateHexagram(28, "大過", "Dà Guò", "Great Preponderance", "Переразвитие", "011110", "䷛", 6, 5,
                "PREPONDERANCE OF THE GREAT. The ridgepole sags to the breaking point.",
                "ПРЕОБЛАДАНИЕ ВЕЛИКОГО. Коньковая балка прогибается до точки разрыва.",
                "The lake rises above the trees: The image of PREPONDERANCE OF THE GREAT.",
                "Озеро поднимается над деревьями: образ ПРЕОБЛАДАНИЯ ВЕЛИКОГО."),
            
            CreateHexagram(29, "坎", "Kǎn", "The Abysmal Water", "Бездна", "010010", "䷜", 3, 3,
                "The Abysmal repeated. If you are sincere, you have success in your heart.",
                "Бездна повторяется. Если ты искренен, у тебя успех в сердце.",
                "Water flows on uninterruptedly: The image of the Abysmal repeated.",
                "Вода течет непрерывно: образ повторяющейся Бездны."),
            
            CreateHexagram(30, "離", "Lí", "The Clinging Fire", "Сияние", "101101", "䷝", 5, 5,
                "THE CLINGING. Perseverance furthers. It brings success. Care of the cow brings good fortune.",
                "СИЯНИЕ. Стойкость благоприятствует. Это приносит успех. Забота о корове приносит счастье.",
                "That which is bright rises twice: The image of FIRE.",
                "То, что ярко, восходит дважды: образ ОГНЯ."),
            
            CreateHexagram(31, "咸", "Xián", "Influence", "Взаимодействие", "011100", "䷞", 6, 4,
                "INFLUENCE. Success. Perseverance furthers. To take a maiden to wife brings good fortune.",
                "ВЛИЯНИЕ. Успех. Стойкость благоприятствует. Взять девушку в жены приносит счастье.",
                "A lake on the mountain: The image of INFLUENCE.",
                "Озеро на горе: образ ВЛИЯНИЯ."),
            
            CreateHexagram(32, "恆", "Héng", "Duration", "Постоянство", "001110", "䷟", 4, 5,
                "DURATION. Success. No blame. Perseverance furthers.",
                "ПОСТОЯНСТВО. Успех. Без вины. Стойкость благоприятствует.",
                "Thunder and wind: The image of DURATION.",
                "Гром и ветер: образ ПОСТОЯНСТВА."),
            
            CreateHexagram(33, "遯", "Dùn", "Retreat", "Отступление", "111100", "䷠", 1, 4,
                "RETREAT. Success. In what is small, perseverance furthers.",
                "ОТСТУПЛЕНИЕ. Успех. В малом стойкость благоприятствует.",
                "Mountain under heaven: The image of RETREAT.",
                "Гора под небом: образ ОТСТУПЛЕНИЯ."),
            
            CreateHexagram(34, "大壯", "Dà Zhuàng", "Great Power", "Великая мощь", "001111", "䷡", 4, 1,
                "THE POWER OF THE GREAT. Perseverance furthers.",
                "СИЛА ВЕЛИКОГО. Стойкость благоприятствует.",
                "Thunder in heaven above: The image of THE POWER OF THE GREAT.",
                "Гром на небе вверху: образ СИЛЫ ВЕЛИКОГО."),
            
            CreateHexagram(35, "晉", "Jìn", "Progress", "Восхождение", "101000", "䷢", 5, 2,
                "PROGRESS. The powerful prince Is honored with horses in large numbers.",
                "ВОСХОЖДЕНИЕ. Могущественный князь награжден лошадьми в большом количестве.",
                "The sun rises over the earth: The image of PROGRESS.",
                "Солнце восходит над землей: образ ВОСХОЖДЕНИЯ."),
            
            CreateHexagram(36, "明夷", "Míng Yí", "Darkening of the Light", "Поражение света", "000101", "䷣", 2, 5,
                "DARKENING OF THE LIGHT. In adversity It furthers one to be persevering.",
                "ПОРАЖЕНИЕ СВЕТА. В невзгодах благоприятно быть стойким.",
                "The light has sunk into the earth: The image of DARKENING OF THE LIGHT.",
                "Свет погрузился в землю: образ ПОРАЖЕНИЯ СВЕТА."),
            
            CreateHexagram(37, "家人", "Jiā Rén", "The Family", "Семья", "110101", "䷤", 5, 5,
                "THE FAMILY. The perseverance of the woman furthers.",
                "СЕМЬЯ. Стойкость женщины благоприятствует.",
                "Wind comes forth from fire: The image of THE FAMILY.",
                "Ветер исходит из огня: образ СЕМЬИ."),
            
            CreateHexagram(38, "睽", "Kuí", "Opposition", "Разлад", "101011", "䷥", 5, 6,
                "OPPOSITION. In small matters, good fortune.",
                "РАЗЛАД. В малых делах счастье.",
                "Above, fire; below, the lake: The image of OPPOSITION.",
                "Вверху огонь; внизу озеро: образ РАЗЛАДА."),
            
            CreateHexagram(39, "蹇", "Jiǎn", "Obstruction", "Препятствие", "010100", "䷦", 3, 4,
                "OBSTRUCTION. The southwest furthers. The northeast does not further.",
                "ПРЕПЯТСТВИЕ. Юго-запад благоприятствует. Северо-восток не благоприятствует.",
                "Water on the mountain: The image of OBSTRUCTION.",
                "Вода на горе: образ ПРЕПЯТСТВИЯ."),
            
            CreateHexagram(40, "解", "Xiè", "Deliverance", "Освобождение", "001010", "䷧", 4, 3,
                "DELIVERANCE. The southwest furthers. If there is no longer anything where one has to go.",
                "ОСВОБОЖДЕНИЕ. Юго-запад благоприятствует. Если больше нет никуда, куда нужно идти.",
                "Thunder and rain set in: The image of DELIVERANCE.",
                "Гром и дождь начинаются: образ ОСВОБОЖДЕНИЯ."),
            
            CreateHexagram(41, "損", "Sǔn", "Decrease", "Убыль", "100011", "䷨", 4, 6,
                "DECREASE combined with sincerity Brings about supreme good fortune without blame.",
                "УБЫЛЬ в сочетании с искренностью приносит высшее счастье без вины.",
                "At the foot of the mountain, the lake: The image of DECREASE.",
                "У подножия горы озеро: образ УБЫЛИ."),
            
            CreateHexagram(42, "益", "Yì", "Increase", "Приумножение", "110001", "䷩", 5, 4,
                "INCREASE. It furthers one To undertake something. It furthers one to cross the great water.",
                "ПРИУМНОЖЕНИЕ. Благоприятно что-то предпринять. Благоприятно пересечь великую воду.",
                "Wind and thunder: The image of INCREASE.",
                "Ветер и гром: образ ПРИУМНОЖЕНИЯ."),
            
            CreateHexagram(43, "夬", "Guài", "Break-through", "Прорыв", "011111", "䷪", 6, 1,
                "BREAK-THROUGH. One must resolutely make the matter known At the court of the king.",
                "ПРОРЫВ. Нужно решительно обнародовать дело при дворе царя.",
                "The lake has risen up to heaven: The image of BREAK-THROUGH.",
                "Озеро поднялось к небу: образ ПРОРЫВА."),
            
            CreateHexagram(44, "姤", "Gòu", "Coming to Meet", "Встреча", "111110", "䷫", 1, 5,
                "COMING TO MEET. The maiden is powerful. One should not marry such a maiden.",
                "ВСТРЕЧА. Девушка сильна. Не следует жениться на такой девушке.",
                "Under heaven, wind: The image of COMING TO MEET.",
                "Под небом ветер: образ ВСТРЕЧИ."),
            
            CreateHexagram(45, "萃", "Cuì", "Gathering Together", "Воссоединение", "011000", "䷬", 6, 2,
                "GATHERING TOGETHER. Success. The king approaches his temple.",
                "ВОССОЕДИНЕНИЕ. Успех. Царь приближается к своему храму.",
                "Over the earth, the lake: The image of GATHERING TOGETHER.",
                "Над землей озеро: образ ВОССОЕДИНЕНИЯ."),
            
            CreateHexagram(46, "升", "Shēng", "Pushing Upward", "Подъем", "000110", "䷭", 2, 5,
                "PUSHING UPWARD has supreme success. One must see the great man.",
                "ПОДЪЕМ имеет высший успех. Нужно увидеть великого человека.",
                "Within the earth, wood grows: The image of PUSHING UPWARD.",
                "Внутри земли растет дерево: образ ПОДЪЕМА."),
            
            CreateHexagram(47, "困", "Kùn", "Oppression", "Истощение", "011010", "䷮", 6, 3,
                "OPPRESSION. Success. Perseverance. The great man brings about good fortune. No blame.",
                "ИСТОЩЕНИЕ. Успех. Стойкость. Великий человек приносит счастье. Без вины.",
                "There is no water in the lake: The image of EXHAUSTION.",
                "В озере нет воды: образ ИСТОЩЕНИЯ."),
            
            CreateHexagram(48, "井", "Jǐng", "The Well", "Колодец", "010110", "䷯", 3, 5,
                "THE WELL. The town may be changed, But the well cannot be changed.",
                "КОЛОДЕЦ. Город может измениться, но колодец не может измениться.",
                "Water over wood: The image of THE WELL.",
                "Вода над деревом: образ КОЛОДЦА."),
            
            CreateHexagram(49, "革", "Gé", "Revolution", "Переворот", "101110", "䷰", 5, 6,
                "REVOLUTION. On your own day You are believed. Supreme success.",
                "ПЕРЕВОРОТ. В твой собственный день тебе верят. Высший успех.",
                "Fire in the lake: The image of REVOLUTION.",
                "Огонь в озере: образ ПЕРЕВОРОТА."),
            
            CreateHexagram(50, "鼎", "Dǐng", "The Caldron", "Жертвенник", "101110", "䷱", 5, 5,
                "THE CALDRON. Supreme good fortune. Success.",
                "ЖЕРТВЕННИК. Высшее счастье. Успех.",
                "Fire over wood: The image of THE CALDRON.",
                "Огонь над деревом: образ ЖЕРТВЕННИКА."),
            
            CreateHexagram(51, "震", "Zhèn", "The Arousing", "Возбуждение", "001001", "䷲", 4, 4,
                "SHOCK brings success. Shock comes--oh, oh! Laughing words--ha, ha!",
                "ПОТРЯСЕНИЕ приносит успех. Потрясение приходит - о, о! Смеющиеся слова - ха, ха!",
                "Thunder repeated: The image of SHOCK.",
                "Гром повторяется: образ ПОТРЯСЕНИЯ."),
            
            CreateHexagram(52, "艮", "Gèn", "Keeping Still", "Сосредоточенность", "100100", "䷳", 4, 4,
                "KEEPING STILL. Keeping his back still So that he no longer feels his body.",
                "СОХРАНЕНИЕ ПОКОЯ. Сохраняя спину неподвижной, так что он больше не чувствует своего тела.",
                "Mountains standing close together: The image of KEEPING STILL.",
                "Горы стоят близко друг к другу: образ СОХРАНЕНИЯ ПОКОЯ."),
            
            CreateHexagram(53, "漸", "Jiàn", "Development", "Постепенное развитие", "110100", "䷴", 5, 4,
                "DEVELOPMENT. The maiden Is given in marriage. Good fortune. Perseverance furthers.",
                "РАЗВИТИЕ. Девушка отдается замуж. Счастье. Стойкость благоприятствует.",
                "On the mountain, a tree: The image of DEVELOPMENT.",
                "На горе дерево: образ РАЗВИТИЯ."),
            
            CreateHexagram(54, "歸妹", "Guī Mèi", "The Marrying Maiden", "Невеста", "001011", "䷵", 4, 6,
                "THE MARRYING MAIDEN. Undertakings bring misfortune. Nothing that would further.",
                "НЕВЕСТА. Предприятия приносят несчастье. Ничто не благоприятствует.",
                "Thunder over the lake: The image of THE MARRYING MAIDEN.",
                "Гром над озером: образ НЕВЕСТЫ."),
            
            CreateHexagram(55, "豐", "Fēng", "Abundance", "Изобилие", "001101", "䷶", 4, 5,
                "ABUNDANCE has success. The king attains abundance. Be not sad. Be like the sun at midday.",
                "ИЗОБИЛИЕ имеет успех. Царь достигает изобилия. Не печалься. Будь как солнце в полдень.",
                "Both thunder and lightning come: The image of ABUNDANCE.",
                "И гром, и молния приходят: образ ИЗОБИЛИЯ."),
            
            CreateHexagram(56, "旅", "Lǚ", "The Wanderer", "Странник", "101100", "䷷", 5, 4,
                "THE WANDERER. Success through smallness. Perseverance brings good fortune to the wanderer.",
                "СТРАННИК. Успех через малость. Стойкость приносит счастье страннику.",
                "Fire on the mountain: The image of THE WANDERER.",
                "Огонь на горе: образ СТРАННИКА."),
            
            CreateHexagram(57, "巽", "Xùn", "The Gentle", "Проникновение", "110110", "䷸", 5, 5,
                "THE GENTLE. Success through what is small. It furthers one to have somewhere to go.",
                "ПРОНИКНОВЕНИЕ. Успех через малое. Благоприятно иметь куда пойти.",
                "Winds following one upon the other: The image of THE GENTLY PENETRATING.",
                "Ветры следуют один за другим: образ МЯГКОГО ПРОНИКНОВЕНИЯ."),
            
            CreateHexagram(58, "兌", "Duì", "The Joyous", "Радость", "011011", "䷹", 6, 6,
                "THE JOYOUS. Success. Perseverance is favorable.",
                "РАДОСТЬ. Успех. Стойкость благоприятна.",
                "Lakes resting one on the other: The image of THE JOYOUS.",
                "Озера покоятся одно на другом: образ РАДОСТИ."),
            
            CreateHexagram(59, "渙", "Huàn", "Dispersion", "Раздробление", "110010", "䷺", 5, 3,
                "DISPERSION. Success. The king approaches his temple. It furthers one to cross the great water.",
                "РАЗДРОБЛЕНИЕ. Успех. Царь приближается к своему храму. Благоприятно пересечь великую воду.",
                "The wind drives over the water: The image of DISPERSION.",
                "Ветер гонит над водой: образ РАЗДРОБЛЕНИЯ."),
            
            CreateHexagram(60, "節", "Jié", "Limitation", "Ограничение", "010011", "䷻", 3, 6,
                "LIMITATION. Success. Galling limitation must not be persevered in.",
                "ОГРАНИЧЕНИЕ. Успех. Мучительное ограничение не должно продолжаться.",
                "Water over lake: The image of LIMITATION.",
                "Вода над озером: образ ОГРАНИЧЕНИЯ."),
            
            CreateHexagram(61, "中孚", "Zhōng Fú", "Inner Truth", "Внутренняя правда", "110011", "䷼", 5, 6,
                "INNER TRUTH. Pigs and fishes. Good fortune. It furthers one to cross the great water.",
                "ВНУТРЕННЯЯ ПРАВДА. Свиньи и рыбы. Счастье. Благоприятно пересечь великую воду.",
                "Wind over lake: The image of INNER TRUTH.",
                "Ветер над озером: образ ВНУТРЕННЕЙ ПРАВДЫ."),
            
            CreateHexagram(62, "小過", "Xiǎo Guò", "Small Preponderance", "Переразвитие малого", "001100", "䷽", 4, 4,
                "PREPONDERANCE OF THE SMALL. Success. Perseverance furthers.",
                "ПРЕОБЛАДАНИЕ МАЛОГО. Успех. Стойкость благоприятствует.",
                "Thunder on the mountain: The image of PREPONDERANCE OF THE SMALL.",
                "Гром на горе: образ ПРЕОБЛАДАНИЯ МАЛОГО."),
            
            CreateHexagram(63, "既濟", "Jì Jì", "After Completion", "Уже конец", "010101", "䷾", 3, 5,
                "AFTER COMPLETION. Success in small matters. Perseverance furthers.",
                "ПОСЛЕ ЗАВЕРШЕНИЯ. Успех в малых делах. Стойкость благоприятствует.",
                "Water over fire: The image of the condition after completion.",
                "Вода над огнем: образ состояния после завершения."),
            
            CreateHexagram(64, "未濟", "Wèi Jì", "Before Completion", "Еще не конец", "101010", "䷿", 5, 3,
                "BEFORE COMPLETION. Success. But if the little fox, after nearly completing the crossing, Gets his tail in the water.",
                "ПЕРЕД ЗАВЕРШЕНИЕМ. Успех. Но если маленькая лиса, почти завершив переправу, намочит хвост в воде.",
                "Fire over water: The image of the condition before transition.",
                "Огонь над водой: образ состояния перед переходом.")
        };
    }

    private static Hexagram CreateHexagram(
        int number, string chinese, string pinyin, string englishName, string russianName,
        string binary, string unicode, int upperTrigram, int lowerTrigram,
        string judgmentEn, string judgmentRu, string imageEn, string imageRu)
    {
        return new Hexagram
        {
            Id = number,
            Number = number,
            ChineseName = chinese,
            Pinyin = pinyin,
            EnglishName = englishName,
            RussianName = russianName,
            Binary = binary,
            Unicode = unicode,
            UpperTrigram = upperTrigram,
            LowerTrigram = lowerTrigram,
            JudgmentEn = judgmentEn,
            JudgmentRu = judgmentRu,
            ImageEn = imageEn,
            ImageRu = imageRu,
            Line1En = $"Line 1: {englishName}",
            Line1Ru = $"Линия 1: {russianName}",
            Line2En = $"Line 2: {englishName}",
            Line2Ru = $"Линия 2: {russianName}",
            Line3En = $"Line 3: {englishName}",
            Line3Ru = $"Линия 3: {russianName}",
            Line4En = $"Line 4: {englishName}",
            Line4Ru = $"Линия 4: {russianName}",
            Line5En = $"Line 5: {englishName}",
            Line5Ru = $"Линия 5: {russianName}",
            Line6En = $"Line 6: {englishName}",
            Line6Ru = $"Линия 6: {russianName}"
        };
    }
}
