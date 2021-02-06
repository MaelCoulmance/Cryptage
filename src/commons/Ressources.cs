// This file is part of the Cryptage Project
// Copyright (C) 2021 Maël Coulmance
// Please check the "StartApp.cs" file or the "License.txt" file for the license.

namespace Cryptage
{
        /// <summary>
        /// This contains all the messages used in this program.
        /// </summary>
    public static class Ressources
    {
        // Program version infos
        public const string CESAR_VERSION = "3.1.2";

        public const string MORSE_VERSION = "2.1.2";

        public const string AERO_VERSION = "2.1.0";

        public const string NAVAJO_VERSION = "2.1.0";

        public const string VIGENERE_VERSION = "1.5.2";

        public const string BINAIRE_VERSION = "1.2.0";

        public const string PROGRAM_VERSION = "3.5.4";



        // About crypto
        public const string CESAR_ABOUT = @"
Code de César

Ce type de cryptage était utilisé par les soldats romains, et
tire son nom de Jules César. Il servait à échanger des informations
entre les différents champs de bataille. Il consiste en un décalage
des lettres vers la gauche selon une clé définie à l'avance.
Par exemple avec une clé valant 3, le message 'bonjour' devient
'ermrxu'. Pour le décripter, il suffit de décaler chaque lettre vers
la droite en respectant la clé.
Ici, il est possible de décaler vers la droite (mode normal) ou vers
la gauche (mode inversé). 
";

        public const string MORSE_ABOUT = @"
Code Morse

Ce type de cryptage a été mis au points par Samuel Morse et 
Alfred Vail. Il permettait de transmettre des messages par 
telegraphe, sous le forme de séquences sonores constituées de sons
cours ou long. L'ordre de ces sons et leur espacement dans le temps
définis la lettre ou le chiffre à transmettre.
Ici, chaque lettre est séparée par un '/' et chaque mot par un espace.
";

        public const string AERO_ABOUT = @"
Code Aéronautique

Ce type de language n'est pas un cryptage à proprement parler. Il 
s'agit en fait d'un moyen de communication plus fiable, permettant
de se faire comprendre à travers les mauvaises transmissions radio
des avions. On remplace simplement les lettres par des mots définis
au préalable.
Par exemple les lettres 'd' et 't' peuvent être difficilement distinguables
si la transmission radio n'est pas bonne. On remplace donc 'd' par 'Delta'
et 't' par 'Tango'. Ainsi, la confusion n'est plus possible.

Ici, deux standarts sont disponibles, le standart international et le 
standart français.
";

        public const string NAVAJO_ABOUT = @"
Code Navajo

Ce type de cryptage est inspiré de la langue Navajo, qui était parlée
par certaines tribus amérindiennes. L'armée américaine s'en est servi
notament pendant la seconde guerre mondiale, car ce langage était
incompréhensible y compris par la pluspart des tribus amérindiennes.
Cela est du au fait que les peuples parlant cette langue vivaient
isolée du reste du pays.
Un opérateur amérindiens (dont le Navajo était la langue natale) était
placé de chaque côté de la ligne de transmission est était chargé de 
traduire les messages.";

        public const string VIGENERE_ABOUT = @"
Clé de Vigenère

Ce type de cryptage à été inventé en 1586 par le cryptographe Blaise de Vigenère.
Il consiste en un chiffrement par substitution polyalphabétique, par rapport a 
une clé donnée.
On se réfère à la table de Vignère (voir menu ? -> table de vigenère), dans laquelle
la premiere ligne réfère aux lettres en claire (lettre du message) et la première
colonne réfère aux lettres cryptées (lettre de la clé).
Pour coder le message, il suffit de prendre la lettre du message sur la première 
ligne, ainsi que la lettre correspondante de la clé sur la première colonne.
La lettre cryptée se trouve à la jonction de la ligne et de la colonne.
Exemple: la lettre ""b"" et la clé ""c"": 
position de ""b"" sur la ligne : 2
position de ""c"" sur la colonne : 3
la lettre cryptée se trouve donc aux coordonées (2;3), soit ""d"".
";

        public const string BINAIRE_ABOUT = @"
Code Binaire

Cet algorithme converti un texte en sont équivalent binaire ou un binaire en sont
équivalent textuel selont l'encodage ASCII (American Standart Code For Information
Interchange).
L'encodage ASCII prend en compte l'alphabet latin, les chiffres arabes ainsi que 
les charactères de ponctuation.
Attention, certains accents ne sont pas pris en compte par l'encodeur
(exemple: ""ë"", ""à"", ""ù"", etc...).";


        public static readonly string PROGRAM_ABOUT = $" Cryptage \n" +
                                                      $"\n\n" +
                                                      $"Version du programme : {PROGRAM_VERSION}\n" +
                                                      $"\n   Version des algorithmes:\n" +
                                                      $"Cesar : {CESAR_VERSION}\n" +
                                                      $"Morse : {MORSE_VERSION}\n" +
                                                      $"AERO  : {AERO_VERSION}\n" +
                                                      $"NAVAJO: {NAVAJO_VERSION}\n" +
                                                      $"VIGENERE: {VIGENERE_VERSION}\n" +
                                                      $"BINAIRE: {BINAIRE_VERSION}" +
                                                      @"

Les caractères spéciaux (. , / : ; etc...) ne sont pas traduisibles et sont donc ignorés
par les algorithmes. Il est possible de ne pas les inclures dans le fichier de destination
via le menu options->caractères non-traduisibles->ignorer.
Attention, à l'exeption du Morse les algorithmes considèrent les chiffres comme des 
caractères speciaux et les affacerons donc si l'option est sélectionnée. ";
        
        
        
        // help menu
        public const string HELP_MENU_AIDE_CRYPTAGE = @"
Ce programme propose différents algorithmes de cryptages. Séléctionnez celui
que vous souhaitez utiliser dans le menu ""Algorithme de cryptage"".

Entrer le nom du fichier contenant le texte à crypter dans le menu ""Fichier source"",
puis le nom du fichier dans lequel la version cryptée de ce texte sera enregistrée dans
le menu ""Fichier cible"".

Differents paramètres de mise en page sont disponibles dans les menu contextuels, pour
plus d'information consulter la page ""Paramètres de mise en page"" du menu ""Aide"".

Pour le code de César et le code Aéronautique, des paramètres spécifiques sont disponibles.
Pour plus d'informations, consultez la page ""Paramètres spéciaux"" du menu ""Aide"".
";

        public const string HELP_MENU_LAYOUT1 = @"
Ce programme propose différents types de mise en page:

- Caractères non traduisibles: 
        Certains caractères spéciaux ("";"", ""."", ""@"", etc...) n'ont pas
        d'équivalent dans les différents types de cryptages, ils seront donc 
        ignorés par le programme.
        Si l'option ""Conserver"" est sélectionnée, ces caractères seront 
        réécrits tels quels dans le fichier de destination.
        Si l'option ""Ignorer"" est sélectionnée, ces caractères ne seront
        pas réécrits dans le fichier de destination.

- Mise en page:
        L'option ""Normale"" permet une mise en page classique dans le fichier
        de destination, c'est à dire que plusieurs mots seront écrits par ligne.
        Leur nombre varie en fonction de l'algorithme choisi, toujours pour 
        permettre une mise en page optimale.
        L'option ""Mot Par Mot"" permet une mise en page spécifique dans le fichier
        de destination, un seul mot sera écrit pour chaque ligne.
";


        public const string HELP_MENU_LAYOUT2 = @"
- Format:
        Cette option permet de choisir quel texte écrire dans le fichier de destination.
        L'option ""Traduction uniquement"" permet de n'écrire que le texte crypter.
        L'option ""Original et traduction"" permet d'écrire le texte original, puis
        le texte crypter. Si cette option est sélectionnée, les deux versions du textes
        seront séparées et annotées dans le fichier de destination.
";

        public const string HELP_MENU_SPECIAL_PARAM1 = @"
Ce programme propose des paramètres spécifiques pour le code de César ainsi que pour
le code Aéronautique:

- Clé de chiffrement pour le code de César:
        Cette option permet de modifier le sens de décalage du texte en fonction de
        la clé de chiffrement.
        Le mode ""normale"" utilise un décalage de la gauche vers la droite (utilisation
        originale du code de César.)
        Le mode ""inversée"" utilise un décalge de la droite vers la gauche.

- Standart pour le code Aeronautique:
        Cette option permet de séléctionner l'alphabet radiophonique à utiliser.
        Le mode ""International"" utilise l'alphabet OACI/OTAN.
        Le mode ""Français"" utilise l'alphabet militaire français.
";

        public const string HELP_MENU_SPECIAL_PARAM2 = @"

- Mot de passe pour le chiffre de Vigenère:
        Le mot de passe doit contenir uniquement des lettres minuscules.
        Il doit également comporter au minimum une lettre, et au maximum
        autant de lettres que comporte le fichier source.
        Attention: si la clé est plus longue que le texte source, il se
        peut que des erreurs se produisent.

- Attention: lorsque vous souhaitez décrypter un texte en utilisant le code de 
  César ou le code Aéronautique, assurez vous que les paramètres spéciaux sont 
  identiques à ceux utilisés lors du cryptage. Dans le cas contraire, le décryptage
  donneras un résultat faux.";

    }
}