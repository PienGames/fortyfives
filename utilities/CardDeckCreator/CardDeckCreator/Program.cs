using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CardDeckCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var suites =  new List<Suite>
            {
                new Suite {Code = "h", Name = "Hearts", SuiteId = 1},
                new Suite {Code = "d", Name = "Diamonds", SuiteId = 2},
                new Suite {Code = "c", Name = "Clubs", SuiteId = 3},
                new Suite {Code = "s", Name = "Spades", SuiteId = 4}
            };

            var heartSuitedCards = getSuitedCards(1);
            var startingCardId = heartSuitedCards.Max(s => s.CardId) + 1;
            var diamondSuitedCards = getSuitedCards(2, startingCardId);
            startingCardId = diamondSuitedCards.Max(s => s.CardId) + 1;
            var clubSuitedCards = getSuitedCards(3, startingCardId);
            startingCardId = clubSuitedCards.Max(s => s.CardId) + 1;
            var spadesSuitedCards = getSuitedCards(4, startingCardId);

            var deck = new Deck {Cards = heartSuitedCards};
            deck.Cards.AddRange(diamondSuitedCards);
            deck.Cards.AddRange(clubSuitedCards);
            deck.Cards.AddRange(spadesSuitedCards);
            deck.Suites = suites;

            var deckJson = JsonConvert.SerializeObject(deck, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var aceOfHearts = heartSuitedCards.First(s => s.FaceValue == "A");


            var orderingRules = new OrderingRules
            {
                HeartsTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 1,
                    CardOrders = getRedTrumpCardOrder(heartSuitedCards, aceOfHearts, true)
                },
                DiamondsTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 2,
                    CardOrders = getRedTrumpCardOrder(diamondSuitedCards, aceOfHearts)
                },
                ClubsTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 3,
                    CardOrders = getBlackTrumpCardOrder(clubSuitedCards, aceOfHearts)
                },
                SpadesTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 4,
                    CardOrders = getBlackTrumpCardOrder(spadesSuitedCards, aceOfHearts)
                },
                HeartsNonTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 1,
                    CardOrders = getRedNonTrumpCardOrder(heartSuitedCards)
                },
                DiamondsNonTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 2,
                    CardOrders = getRedNonTrumpCardOrder(diamondSuitedCards, true)
                },
                ClubsNonTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 3,
                    CardOrders = getBlackNonTrumpCardOrder(clubSuitedCards)
                },
                SpadesNonTrumpCardOrderings = new CardOrdering
                {
                    SuiteId = 4,
                    CardOrders = getBlackNonTrumpCardOrder(spadesSuitedCards)
                }
            };

            var orderingRulesJson = JsonConvert.SerializeObject(orderingRules, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }


        private static List<CardOrder> getRedTrumpCardOrder(List<Card> suitedCards, Card aceOfHearts, bool isHearts = false)
        {
            var cardOrder = getRedTrumpNonFaceCardOrder(suitedCards);
            cardOrder.AddRange(getBossCardOrder(suitedCards, aceOfHearts, isHearts));
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getBlackTrumpCardOrder(List<Card> suitedCards, Card aceOfHearts)
        {
            var cardOrder = getBlackTrumpNonFaceCardOrder(suitedCards);
            cardOrder.AddRange(getBossCardOrder(suitedCards, aceOfHearts));
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getRedNonTrumpCardOrder(List<Card> suitedCards, bool isDiamonds = false)
        {
            var cardOrder = getNonTrumpFaceCardOrder(suitedCards);
            cardOrder.AddRange(getRedNonTrumpNonFaceCardOrder(suitedCards, isDiamonds));
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getBlackNonTrumpCardOrder(List<Card> suitedCards)
        {
            var cardOrder = getNonTrumpFaceCardOrder(suitedCards);
            cardOrder.AddRange(getBlackNonTrumpNonFaceCardOrder(suitedCards));
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getNonTrumpFaceCardOrder(List<Card> suitedCards)
        {
            return new List<CardOrder>()
            {
                getCardOrder(suitedCards, "K", 0),
                getCardOrder(suitedCards, "Q", 1),
                getCardOrder(suitedCards, "J", 2),
            };
        }


        private static List<CardOrder> getBlackTrumpNonFaceCardOrder(List<Card> suitedCards)
        {
            var cardOrder = new List<CardOrder>
            {
                getCardOrder(suitedCards, "10", 13),
                getCardOrder(suitedCards, "9", 12),
                getCardOrder(suitedCards, "8", 11),
                getCardOrder(suitedCards, "7", 10),
                getCardOrder(suitedCards, "6", 9),
                getCardOrder(suitedCards, "4", 8),
                getCardOrder(suitedCards, "3", 7),
                getCardOrder(suitedCards, "2", 6),
            };
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getRedTrumpNonFaceCardOrder(List<Card> suitedCards)
        {
            var cardOrder = new List<CardOrder>
            {
                getCardOrder(suitedCards, "10", 6),
                getCardOrder(suitedCards, "9", 7),
                getCardOrder(suitedCards, "8", 8),
                getCardOrder(suitedCards, "7", 9),
                getCardOrder(suitedCards, "6", 10),
                getCardOrder(suitedCards, "4", 11),
                getCardOrder(suitedCards, "3", 12),
                getCardOrder(suitedCards, "2", 13),
            };

            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static List<CardOrder> getBlackNonTrumpNonFaceCardOrder(List<Card> suitedCards)
        {
            var cardOrder = new List<CardOrder>
            {
                getCardOrder(suitedCards, "10", 12),
                getCardOrder(suitedCards, "9", 11),
                getCardOrder(suitedCards, "8", 10),
                getCardOrder(suitedCards, "7", 9),
                getCardOrder(suitedCards, "6", 8),
                getCardOrder(suitedCards, "5", 7),
                getCardOrder(suitedCards, "4", 6),
                getCardOrder(suitedCards, "3", 5),
                getCardOrder(suitedCards, "2", 4),
                getCardOrder(suitedCards, "A", 3),
            };
            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        private static IEnumerable<CardOrder> getRedNonTrumpNonFaceCardOrder(List<Card> suitedCards, bool isDiamonds = false)
        {
            var cardOrder = new List<CardOrder>
            {
                getCardOrder(suitedCards, "10", 3),
                getCardOrder(suitedCards, "9", 4),
                getCardOrder(suitedCards, "8", 5),
                getCardOrder(suitedCards, "7", 6),
                getCardOrder(suitedCards, "6", 7),
                getCardOrder(suitedCards, "5", 8),
                getCardOrder(suitedCards, "4", 9),
                getCardOrder(suitedCards, "3", 10),
                getCardOrder(suitedCards, "2", 11),
            };

            if (isDiamonds)
            {
                cardOrder.Add(getCardOrder(suitedCards, "A", 12));
            }

            return cardOrder.OrderBy(s => s.Order).ToList();
        }

        

        private static IEnumerable<CardOrder> getBossCardOrder(List<Card> suitedCards, Card aceOfHearts, bool isHearts = false)
        {
            var cardOrder = new List<CardOrder>
            {
                getCardOrder(suitedCards, "5", 0),
                getCardOrder(suitedCards, "J", 1),
                new CardOrder
                {
                    CardId = aceOfHearts.CardId,
                    FaceValue = "A",
                    SuiteCode = "h",
                    Order = 2
                },
                getCardOrder(suitedCards, "K", 4),
                getCardOrder(suitedCards, "Q", 5),
            };

            if (!isHearts)
            {
                cardOrder.Add(getCardOrder(suitedCards, "A", 3));
            }

            return cardOrder.OrderBy(s => s.Order).ToList();
        }


        private static CardOrder getCardOrder(IEnumerable<Card> suitedCards, string faceValue, int order)
        {
            var card = suitedCards.First(s => s.FaceValue == faceValue);
            var suiteCode = "h";
            switch (card.SuiteId)
            {
                case 1:
                    suiteCode = "h";
                    break;
                case 2:
                    suiteCode = "d";
                    break;
                case 3:
                    suiteCode = "c";
                    break;
                case 4:
                    suiteCode = "s";
                    break;
            }

            return new CardOrder
            {
                CardId = card.CardId,
                SuiteCode = suiteCode,
                Order = order,
                FaceValue = faceValue
            };
        }


        private static List<Card> getSuitedCards(int suiteId, int startingCardId = 1)
        {
            var suitedCards = new List<Card>
            {
                new Card
                {
                    FaceValue = "A",
                    Name = "Ace",
                    SuiteId = suiteId,
                    CardId =  startingCardId++
                },
                new Card
                {
                    FaceValue = "K",
                    Name = "King",
                    SuiteId = suiteId,
                    CardId =  startingCardId++
                },
                new Card
                {
                    FaceValue = "Q",
                    Name = "Queen",
                    SuiteId = suiteId,
                    CardId =  startingCardId++
                },
                new Card
                {
                    FaceValue = "J",
                    Name = "Jack",
                    SuiteId = suiteId,
                    CardId =  startingCardId++
                }
            };

            for (int i = 2; i <= 10; i++)
            {
                suitedCards.Add(new Card
                {
                    FaceValue = i.ToString(),
                    Name = i.ToString(),
                    SuiteId = suiteId,
                    CardId = startingCardId++
                });
            }

            return suitedCards;
        }
    }

    public class Deck
    {
        public List<Card> Cards { get; set; }
        public List<Suite> Suites { get; set; }


    }

    public class OrderingRules
    {
        public CardOrdering HeartsTrumpCardOrderings { get; set; }
        public CardOrdering DiamondsTrumpCardOrderings { get; set; }
        public CardOrdering SpadesTrumpCardOrderings { get; set; }
        public CardOrdering ClubsTrumpCardOrderings { get; set; }

        public CardOrdering HeartsNonTrumpCardOrderings { get; set; }
        public CardOrdering DiamondsNonTrumpCardOrderings { get; set; }
        public CardOrdering SpadesNonTrumpCardOrderings { get; set; }
        public CardOrdering ClubsNonTrumpCardOrderings { get; set; }
    }


    public class Card
    {
        public int SuiteId { get; set; }
        public string FaceValue { get; set; }
        public string Name { get; set; }

        public int CardId { get; set; }

    }


    public class Suite
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int SuiteId { get; set; }
    }

    public class CardOrdering
    {
        public int SuiteId { get; set; }
        public List<CardOrder> CardOrders { get; set; }
    }

    public class CardOrder
    {
        public int CardId { get; set; }
        public int Order { get; set; }

        public string FaceValue { get; set; }

        public string SuiteCode { get; set; }
    }
}

