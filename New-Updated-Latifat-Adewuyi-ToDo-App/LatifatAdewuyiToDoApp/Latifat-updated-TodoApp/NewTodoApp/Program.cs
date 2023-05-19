using Microsoft.VisualBasic;
using NewTodoApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace NewToDoApp
{

    public class Program
    {

        static void Main(string[] args)
        {
            SignUpSignIn signUpSignIn = new SignUpSignIn();
            signUpSignIn.Menu();
        }
    }
}




