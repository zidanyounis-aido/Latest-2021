//
// Dynamsoft JavaScript Library for Basic Initiation of Dynamic Web TWAIN
// More info on DWT: http://www.dynamsoft.com/Products/WebTWAIN_Overview.aspx
//
// Copyright 2017, Dynamsoft Corporation 
// Author: Dynamsoft Team
// Version: 13.2
//
/// <reference path="dynamsoft.webtwain.initiate.js" />
var Dynamsoft = Dynamsoft || { WebTwainEnv: {} };

Dynamsoft.WebTwainEnv.AutoLoad = true;

///
Dynamsoft.WebTwainEnv.Containers = [{ContainerId:'dwtcontrolContainer', Width:270, Height:350}];

/// If you need to use multiple keys on the same server, you can combine keys and write like this 
/// Dynamsoft.WebTwainEnv.ProductKey = 'key1;key2;key3';
Dynamsoft.WebTwainEnv.ProductKey = '3A52759B9727863DB422406070AB9EFB3F351E5F7E7F212A37C5C5BC4E5871F10BF0507A167D831937BBE9E4F4246A710BF0507A167D831963039817078A437C3F351E5F7E7F212AA91AE8F1EFDD0A2B40000000;t0068WQAAAGL/LnzkMle57pmrFHayceZrttaM+sFSEdh2py6H3qxRrgCsLs0iutollfljlBN8EPEtMQUQy8nzfS9sJQG8x/I=';

///
Dynamsoft.WebTwainEnv.Trial = true;


///
Dynamsoft.WebTwainEnv.ActiveXInstallWithCAB = false;

///
// Dynamsoft.WebTwainEnv.ResourcesPath = 'Resources';

/// All callbacks are defined in the dynamsoft.webtwain.install.js file, you can customize them.
// Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', function(){
// 		// webtwain has been inited
// });

