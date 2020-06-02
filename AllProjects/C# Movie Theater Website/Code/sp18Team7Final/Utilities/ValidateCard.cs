using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sp18Team7Final.Models;
using sp18Team7Final.DAL;

namespace sp18Team7Final.Utilities
{
    public class ValidateCard
    {
        public static bool Validation(String cardnumber)
        {
            bool confirmed = true;
            if (cardnumber.Length < 15 || cardnumber.Length > 16)
            {
                confirmed = false;
            }
            if (cardnumber.Length == 15)
            {
                
                    confirmed = true;
                
            }
            if(cardnumber.Length == 16)
            {
                if (cardnumber.StartsWith("54"))
                {
                    confirmed = true;
                }
                else if (cardnumber.StartsWith("4"))
                {
                    confirmed = true;
                }
                else if (cardnumber.StartsWith("6"))
                {
                    confirmed = true;
                }
                else
                {
                    confirmed = false;
                }
            }
            return confirmed;
        }

        public static CardType GetCardType(String cardnumber)
        {
            CardType cardtype = new CardType();
            if (cardnumber.StartsWith("54"))
            {
                cardtype = CardType.Mastercard;
            }
            if(cardnumber.StartsWith("4"))
            {
                cardtype = CardType.Visa;
            }
            if (cardnumber.StartsWith("6"))
            {
                cardtype = CardType.Discover;
            }
            if(cardnumber.Length == 15)
            {
                cardtype = CardType.Amex;
            }
            return cardtype;
        }

        public static String CreateHiddenNumberString(String cardnumber)
        {
            //TODO: convert card number to only show last 4 digits and all other digits as "*"
            String hiddenstring = "";
            var length = cardnumber.Length;
            hiddenstring = new string('*', length - 4) + cardnumber.Substring(length - 4);

            CardType TypeOfCard = GetCardType(cardnumber);
            switch (TypeOfCard)
            {
                case CardType.Mastercard:
                    hiddenstring += " MASTERCARD";
                    break;
                case CardType.Visa:
                    hiddenstring += " VISA";
                    break;
                case CardType.Discover:
                    hiddenstring += " DISCOVER";
                    break;
                case CardType.Amex:
                    hiddenstring += " AMEX";
                    break;
            }
            return (hiddenstring);
        }
    }
}