﻿// See https://aka.ms/new-console-template for more information
using EEKDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();

DapperExample dapperExample = new DapperExample();
////dapperExample.Read();
//dapperExample.Create("Dapper","EK","jakdfaiufhoaifla");
//dapperExample.Update(6,"Update","EK","testing");
dapperExample.Delete(13);


Console.ReadKey();

