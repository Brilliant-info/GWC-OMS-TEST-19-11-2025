/*
=============================================
|	JS App: Brilliant WMS App navigator		|
|	Version: 1.0							|
|	Published on: 17 Oct 2022				|
=============================================
*/

var wmsApp = null;
var wmsAppPage = null;
var wmsAppPath = null;
// var wmsApiPath = "http://173.212.244.46/BWMSWebAPITest/api/staging/v1/";
//var wmsAppFiles = "http://173.212.244.46/bwmsAppfiles/";

var wmsApiPath = "http://localhost:50068/api/Dev/v1/";

//var wmsApiPath = "https://testoms.gwclogistics.com/GWCTESTRMSWEBAPI/api/Dev/v1/";
//var S2SwmsApiPath = "https://testoms.gwclogistics.com/S2SWEBAPI/";

var S2SwmsApiPath = "https://localhost:7127/api/";


var wmsAppDefaultPageFunction = null;
var isWmsPageLoading = "no";
var wmsDefaultPage = "";
$(document).ready(function () {
	var getAppPage = location.href;
	if (getAppPage.indexOf('WMSApp.html') > -1) {
		if (sessionStorage["userid"] != null && sessionStorage["userid"] != '') {
			var apiPath = 'Route/RootConfig.json?tm=' + getTimeStamp();
			callHttpUrl(apiPath, null, function (data) {
				wmsAppPath = data;
				// if((sessionStorage["isSetupDone"]) && (sessionStorage["isSetupDone"] == 'yes')){
				getWMSMenu(true);
				// }else{
				// 	getWMSMenu(false);
				// }
			}, function () {
				alert('Unable to load navigation!!');
			});
		} else {
			location.href = 'Public/login.html';
		}
	}
});

function getWMSMenu(isSetupDone) {
	$('#ctl00_dvMenu ul').html('');
	var getUserId = mBrillWmsSession.getUserId();
	var getUserRole = mBrillWmsSession.getUserType();
	var apiPath = wmsApiPath + "Menupage/GetMenu";
	var postData = {
		"UserID": getUserId,
		"UserType": getUserRole,
		"ParentId": "0"
	};
	callHttpUrl(apiPath, postData, function (data) {
		debugger;
		var getStatus = data.Status;
		var getStatusCode = data.StatusCode;
		if (getStatusCode.toLocaleLowerCase() == 'success') {
			var getUserLoginData = data.Result.Table;
			for (var i = 0; i < getUserLoginData.length; i++) {
				var menuLabel = getUserLoginData[i].MenuLable;
				var menuPage = getUserLoginData[i].MenuURL;
				var menuIcon = getUserLoginData[i].MenuIcon;
				addMenu(menuLabel, menuPage, menuIcon);
			}
			wmsNavigatePage('CommonLibrary');
			//wmsNavigatePage('Dashboard');
			wmsDefaultPage = getUserLoginData[0].MenuURL;
			//wmsNavigatePage(getUserLoginData[0].MenuURL);
		} else {
			var getMessage = data.Result.Message;
			alert(getMessage);
		}
		$('[data-toggle="tooltip"]').tooltip();
	});
}

function getWMSMenu_static_old(isSetupDone) {
	$('#ctl00_dvMenu ul').html('');
	if (isSetupDone) {
		// addMenu(Label, Page, Icon);
		//addMenu('WMS Setup','WMSSetup','fas fa-user-cog');
		addMenu('Dashboard', 'Dashboard', 'fas fa-home');
		//addMenu('GatePass','GatePass','fas fa-ticket-alt');
		addMenu('Inward', 'Inward', 'fas fa-truck-moving');
		addMenu('Outward', 'Outward', 'fas fa-truck-moving fa-flip-horizontal');
		//addMenu('Transfer','Transfer','fas fa-exchange-alt');
		addMenu('Inventory', 'Inventory', 'fas fa-boxes');
		//addMenu('Barcode Formatting','BarcodeFormatting','fas fa-boxes');
		addMenu('Subscription', 'Subscription', 'fas fa-shopping-cart');
		addMenu('Administrator', 'Administrator', 'fas fa-user-cog');

		// addMenu('CompanyMaster','CompanyMaster','fas fa-user-cog');
		wmsNavigatePage('CommonLibrary');
		wmsNavigatePage('Dashboard');
	} else {
		addMenu('WMS Setup', 'Setup', 'fas fa-cog');
		wmsNavigatePage('Setup');
	}
	$('[data-toggle="tooltip"]').tooltip();
}

function addMenu(Label, Page, Icon) {
	var menuItem = '<li><a onclick="wmsNavigatePage(\'' + Page + '\');" data-toggle="tooltip" data-placement="right" data-html="true" title="<i class=\'' + Icon + '\'></i> ' + Label + '"><i class="' + Icon + '"></i><span>' + Label + '</span></a></li>';
	$('#ctl00_dvMenu ul').append(menuItem);
}

function wmsNavigatePage(objPage) {
	if (isWmsPageLoading == "no") {
		$('body').removeClass('wmsNavActive');
		hideThemeWMSNav();
		//$('#appNavClose').click();
		if (objPage != '#' && objPage != 'Administrator') {
			isWmsPageLoading = "yes";
			hideThemeWMSNav();
			wmsAppPage = objPage;
			init = null;
			// GET MODULES
			var getAllModules = wmsAppPath[objPage][0].Modules[0];
			var currentModules = [];
			var count = 0;
			// Check if every key has its own property
			for (key in getAllModules) {
				if (getAllModules.hasOwnProperty(key))

					// If the key is found, add it to the total length
					currentModules.push(key);
				count++;
			}
			objectLenght = count;
			var currentModuleCount = 0;
			var totalModule = currentModules.length;

			//	$.getScript('Module/'+ objPage + "/" + wmsAppPath[objPage][0].Default +'.js');
			//	loadAllModules(objPage, currentModules, currentModuleCount, totalModule);
			loadModule(objPage, currentModules, currentModuleCount, totalModule);

		} else if (objPage == 'Administrator') {
			isWmsPageLoading = "yes";
			var currentModules = [];
			currentModules.push("Administrator");
			var currentModuleCount = 0;
			var totalModule = currentModules.length;
			loadModule(objPage, currentModules, currentModuleCount, totalModule);
		}
	}
	return false;
}

function loadAdminMasters(masterPage, onMasterLoad) {
	if (isWmsPageLoading == "no") {
		isWmsPageLoading = "yes";
		var objPage = "Administrator";
		var arrModules = masterPage.replace('pnl', '');
		$('#divCommonLibrary').html('');
		$.get('Module-Style/' + objPage + "/" + arrModules + '.css?tm=' + getTimeStamp(), function (cssdata) {
			var lnkCss = '<style>' + cssdata + '</style>';
			$('#divCPHolder_Form').append(lnkCss);
			$.get('Views/' + objPage + "/" + arrModules + '.html?tm=' + getTimeStamp(), function (data) {
				$('#divCPHolder_Form').append(data);
				$.getScript('Module/' + objPage + "/" + arrModules + '.js?tm=' + getTimeStamp(), function () {
					if (onMasterLoad != null) {
						onMasterLoad();
						isWmsPageLoading = "no";
						var resizeAfterDelay = setTimeout(function(){
							$(window).resize();
						},500);
					}
				});
			});
		});
	}
}

function loadModule(objPage, currentModules, currentModuleCount, totalModule) {
	if (objPage == 'CommonLibrary') {
		$('#divCommonLibrary').html('');
	} else {
		$('#divCPHolder_Form').html('');
	}

	loadAllCSS(objPage, currentModules, currentModuleCount, totalModule);
}

function loadAllModules(objPage, arrModules, currentModule, totalModule) {
	if (currentModule < totalModule) {
		var setModuleIndex = (totalModule - 1) - currentModule;
		$.getScript('Module/' + objPage + "/" + arrModules[setModuleIndex] + '.js?tm=' + getTimeStamp(), function () {
			currentModule = currentModule + 1;
			console.log("Loading Module: " + arrModules[setModuleIndex] + ".js");
			loadAllModules(objPage, arrModules, currentModule, totalModule);
		});
	} else {
		isWmsPageLoading = "no";
		if(objPage == 'CommonLibrary'){
			wmsNavigatePage(wmsDefaultPage);
		}
		var resizeAfterDelay = setTimeout(function(){
			$(window).resize();
		},500);
		
	}
}

function loadAllCSS(objPage, arrModules, currentModule, totalModule) {
	if (currentModule < totalModule) {
		var setModuleIndex = (totalModule - 1) - currentModule;
		// var setModuleIndex = currentModule;
		$.get('Module-Style/' + objPage + "/" + arrModules[setModuleIndex] + '.css?tm=' + getTimeStamp(), function (cssdata) {
			currentModule = currentModule + 1;
			var lnkCss = '<style>' + cssdata + '</style>';
			if (objPage == 'CommonLibrary') {
				$('#divCommonLibrary').append(lnkCss);
			} else {
				$('#divCPHolder_Form').append(lnkCss);
			}

			console.log("Loading CSS: " + arrModules[setModuleIndex] + ".css");
			loadAllCSS(objPage, arrModules, currentModule, totalModule);
		});
	} else {
		currentModule = 0;
		loadAllHtml(objPage, arrModules, currentModule, totalModule);
	}
}

function loadAllHtml(objPage, arrModules, currentModule, totalModule) {
	if (currentModule < totalModule) {
		var setModuleIndex = (totalModule - 1) - currentModule;
		$.get('Views/' + objPage + "/" + arrModules[setModuleIndex] + '.html?tm=' + getTimeStamp(), function (data) {
			currentModule = currentModule + 1;
			if (objPage == 'CommonLibrary') {
				$('#divCommonLibrary').append(data);
			} else {
				$('#divCPHolder_Form').append(data);
			}

			console.log("Loading HTML: " + arrModules[setModuleIndex] + ".html");
			loadAllHtml(objPage, arrModules, currentModule, totalModule);
		});
	} else {
		currentModule = 0;
		loadAllModules(objPage, arrModules, currentModule, totalModule);
	}
}

// function wmsLoadLayout(myCallBack){
// 	$.get( "Module-Style/" + wmsAppPage + "/" + wmsAppPath[wmsAppPage][0].Default + ".css", function( cssdata ) {
// 		$.get( "Views/" + wmsAppPage + "/" + wmsAppPath[wmsAppPage][0].Default +".html", function( data ) {
// 			var lnkCss = '<style>'+ cssdata +'</style>';
// 			$('#divCPHolder_Form').html(lnkCss + data);
// 			adjustGrid();
// 			if(myCallBack != null){
// 				myCallBack();
// 			}
// 		});
// 	});
// }

function wmsLoadLayout(myCallBack) {
	wmsAppDefaultPageFunction = null;
	adjustGrid();
	if (myCallBack != null) {
		myCallBack();
	}
}

function showWMSThemeLoading() {
	$('#themeWMSLoading').css('display', 'block');
}
function hideWMSThemeLoading() {
	$('#themeWMSLoading').css('display', 'none');
}

function callHttpUrl(apiPath, postData, fnLoadCallBack, fnOnErrorCallBack) {
	showWMSThemeLoading();
	if (postData != null) {
		jQuery.ajax({
			type: "POST",
			url: apiPath,
			beforeSend: function (request) {
				// request.setRequestHeader("apikey", "************************");
				showWMSThemeLoading();
			},
			data: postData,
			success: function (data, textStatus, jQxhr) {
				hideWMSThemeLoading();

				if (fnLoadCallBack != null) {
					fnLoadCallBack(data);
				}
			},
			error: function (jqXhr, textStatus, errorThrown) {
				hideWMSThemeLoading();
				// alert('Unable to connect server!!');

				if (fnOnErrorCallBack != null) {
					fnOnErrorCallBack();
				}
			}
		});
	} else {
		jQuery.ajax({
			type: "GET",
			url: apiPath,
			beforeSend: function (request) {
				// request.setRequestHeader("apikey", "************************");
				// showWMSThemeLoading();
			},
			success: function (data, textStatus, jQxhr) {
				hideWMSThemeLoading();

				if (fnLoadCallBack != null) {
					fnLoadCallBack(data);
				}
			},
			error: function (jqXhr, textStatus, errorThrown) {
				hideWMSThemeLoading();
				// alert('Unable to connect server!!');

				if (fnOnErrorCallBack != null) {
					fnOnErrorCallBack();
				}
			}
		});
	}
}

function getTimeStamp() {
	var tmDate = new Date();
	var tmChar = '$';
	var getDate = tmDate.getDate();
	var getMonth = tmDate.getMonth();
	var getYear = tmDate.getFullYear();
	var getHours = tmDate.getHours();
	var getMins = tmDate.getMinutes();
	var getSec = tmDate.getSeconds();
	var timeStamp = getDate + tmChar + getMonth + tmChar + getYear + tmChar + getHours + tmChar + getMins + tmChar + getSec;
	return timeStamp;
}

function callHttpCoreUrl(apiPath, postData, fnLoadCallBack, fnOnErrorCallBack) {
    showWMSThemeLoading();
    if (postData != null) {
        jQuery.ajax({
            type: "POST",
            url: apiPath,
            contentType: 'application/json',
            data: JSON.stringify(postData),
            beforeSend: function (request) {
                // request.setRequestHeader("apikey", "************************");
                showWMSThemeLoading();
            },


            success: function (data, textStatus, jQxhr) {
                hideWMSThemeLoading();

                if (fnLoadCallBack != null) {
                    fnLoadCallBack(data);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                hideWMSThemeLoading();
                // alert('Unable to connect server!!');

                if (fnOnErrorCallBack != null) {
                    fnOnErrorCallBack();
                }
            }
        });
    } else {
        jQuery.ajax({
            type: "GET",
            url: apiPath,
            beforeSend: function (request) {
                // request.setRequestHeader("apikey", "************************");
                // showWMSThemeLoading();
            },
            success: function (data, textStatus, jQxhr) {
                hideWMSThemeLoading();

                if (fnLoadCallBack != null) {
                    fnLoadCallBack(data);
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {
                hideWMSThemeLoading();
                // alert('Unable to connect server!!');

                if (fnOnErrorCallBack != null) {
                    fnOnErrorCallBack();
                }
            }
        });
    }
}
