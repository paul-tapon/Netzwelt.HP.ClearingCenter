// ========================================
// Core JavaScript Utilities for the HP Experience Center
// Last modified: Thu May 03 2012 by Chris Bobbett
// ========================================


// ---------------------------------------------------
// Image Viewer: Used to swap images in the Inspire us overlays
// Requires: jQuery 1.7+
// ---------------------------------------------------
var contentToggler = function(options)
{
	//  Setup the default options to use if none or only some are provided
	var defaultOptions = 
	{
		context: document.body,
		triggers: ".triggers a",
		targets: ".targets img"
	};

	// Use the provided option values or default values if none provided 
	var context = (options && options.context) ? options.context : defaultOptions.context;
	var triggers = (options && options.triggers) ? $(options.triggers, context) : $(defaultOptions.triggers, context);
	var targets = (options && options.targets) ? $(options.targets, context) : $(defaultOptions.targets, context);

	// Hide all the targets except the first one and set first trigger to .active
	targets.hide().first().show();
	triggers.first().addClass('active');


	// Add event handlers for triggers
	triggers.on(
	{
		'click': function(event)
		{
			event.preventDefault();
			var aTag = $(event.target).parent('a');
			if (aTag.hasClass('active'))
			{
				return;
			}
			else
			{
				// Reset the .active class on all triggers and hide all targets
				triggers.removeClass('active');
				targets.hide();

				// Set the item clicked to be .active
				aTag.addClass('active');

				// Fade in the corresponding image requested from the item clicked HREF
				var target = aTag.attr('href');
				$(target).fadeIn();
			}
		}
	});
};


// ---------------------------------------------------
// Normalize Column Heights: Used to even up column heights
// Requires: jQuery 1.0+
// ---------------------------------------------------
function normalizeColumnHeights(columns)
{
	// Equalize the left, center and right column heights
	var columnsToNormalize = $(columns);
	var columnHeights = [];
	var maxHeight;

	columnsToNormalize.each(function(el)
	{
		var elHeight = $(this).height();
		columnHeights.push(elHeight);
	});

	maxHeight = columnHeights.sort().pop();
	columnsToNormalize.css('height',maxHeight);
}


// ---------------------------------------------------
// Modal abstract loader: Load inspire us modal abstracts and apply Fancybox interactivity
// Requires: jQuery 1.6+
// ---------------------------------------------------
function loadModalAbstracts(filepath, abstracts, columns, modalTriggerSelector)
{
	var numColumns = columns.length;
	var numAbstracts = abstracts.length;
	
	// Fetch each modal abstract and append to the appropriate column
	for (var i=0; i<numAbstracts; i++)
	{		
		var colNum = i%numColumns;
		var colElToAppend = columns[colNum];

		$.ajax(
		{
			url: filepath + abstracts[i],
			async: false,
			dataType: 'html',
			success: function(response)
			{
				$(colElToAppend).append(response);
			}
		});

	}
	
	//  After abstracts are fetched, attach the Fancybox event listener to the trigger elements to launch the modal
	$(modalTriggerSelector).fancybox(
	{
		width: 1000,
		height: 570,
		padding: 0,
		autoSize: false,
		fitToView: false,
		scrolling: 'no',
		closeBtn: false,
		type: 'ajax',
		tpl:
		{
			error: '<div class="modal modal-error"><div class="close-btn"><a href="#none" onClick="jQuery.fancybox.close(); return false;">X</a></div><h2>Sorry. The requested URL could not be found.</h2><p>Please contact <a href="mailto:hpexperience@hp.com">hpexperience@hp.com</a> to inform them of the problem.</p><p>- Thank you</p> </div>'
		}
	});
}


// ---------------------------------------------------
// Show Top Nav Menu
// Requires: jQuery 1.0+
// ---------------------------------------------------
function showTopNavMenu(event)
{
	$(event.target).parents("nav").addClass("flyout-open").children('.flyout-menu').removeClass('hide');
}


// ---------------------------------------------------
// Hide Top Nav Menu
// Requires: jQuery 1.0+
// ---------------------------------------------------
function hideTopNavMenu()
{
	$(this).removeClass('flyout-open').children('.flyout-menu').addClass('hide');
}


// ---------------------------------------------------
// Checking for DOM READY, attaching event observers
// Requires: jQuery 1.7+, Fancybox 2.0.6
// ---------------------------------------------------
$(function()
{
	//  Activate top nav drop downs
	var topNav = 
	(
		function (nav1, nav2)
		{
			if (nav1.length === 1)
			{
				return nav1;
			}
			else if (nav2.length === 1)
			{
				return nav2;
			}
		}
	)($('#top-nav').first(), $('#top-nav-new').first());

	if (topNav)
	{
		// Shows/hides the menus as a user moves over the topnav
		var topNavItems = topNav.children('nav');
		topNavItems.on(
		{
			'mouseover': showTopNavMenu,
			'mouseout':  hideTopNavMenu
		});
	}
});


//------------------------------------
// Common functions used on the site.
// Source: utils.js
// Original Author: CDW
//------------------------------------

//Global variables, used on forms.
var place = getQueryVariable('place');
var url = getQueryVariable('url');
var sval = "Search";

//Run common functions on page load.
function init() {	
	if (document.getElementById('page').className.match('form')) { //setting some options on page if a form.
		formUtils();
	}
}

function formUtils() {
	//default fallback function if page specific formUtils() does not exist. DO NO DELETE.
}

//Get location on forms to return user to previous location
function formsLocation(goURL,ifsearch) {
	var place = escape(document.title);
	var fromURL = window.location.toString();
	if (ifsearch == null ) {
		window.location = goURL+"?place="+place+"&url="+escape(fromURL);
	} else if (ifsearch == "yes") {
		window.location = goURL;
	}
}
//Write link on 'thanks' pages of forms.
function writeReturnLink(place,url) {
	if (place != null && url != null) {
		document.write("Return to: <a href='"+unescape(url)+"'>"+unescape(place)+"</a>.");
	}
}

//Form - select box go script
function getOption(form) {
	if (form.options[form.selectedIndex].value != "#") {
		location = form.options[form.selectedIndex].value;
	}	
}
//Check email for basic struture like an @ sign on newsletter signup
function chkEmail(form) {
	if (!form.EmailAddress.value.match('@')) {
		alert("Please provide a properly formatted e-mail address.");
		form.EmailAddress.focus();
		return false;
	} else {
		return true;
	}
}

//check validatity of email address on login forms
function isValidEmail(email) {
     var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/; 
     return emailPattern.test(email);
}

//Form Validation - basic check to see of field empty or not checked.
function formValidation(form,formErrors) {	
	var fields = form.elements;
	var formErrors2 = "";
	var details = "<ul>";
	
	if (formErrors == null) { // chk if any form specific errors have been passed in
		formErrors2 = "";
	} else if (formErrors != "") {
		formErrors2 = formErrors;
	}	//alert("details = " + details + "<br>formErrors = " + formErrors2);
	var boxli, radli = 0;
	for (var i = 0; i < fields.length; i++ ){
		if (fields[i].className.match("required")) {
			//chk text and textarea fields
			if ((fields[i].type == 'text' || fields[i].type == 'textarea' || fields[i].type == 'password') && !fields[i].value.length > 0) {
				details += "<li>"+fields[i].title+" is blank.</li>";
			//chk radio buttons
			} else if (fields[i].type == 'radio' ) {
				//do something
				var rname = fields[i].name;
				var radio = document.getElementsByName(rname);
				for (cnt = x = 0; x < radio.length; x++ ) {
					if (radio[x].checked) cnt++; 
				}
				if (cnt == 0 && (!details.match(fields[i].title))) { //check to see if li already in 'details'
					radli++;
					details += "<li>"+fields[i].title+" does not have a selection.</li>";
				}
			} 
			//check checkboxes (single or groups)
			else if (fields[i].type == 'checkbox') { 
				var boxName = fields[i].name;
				var boxes = document.getElementsByName(boxName);
				for (cnt = x = 0; x < boxes.length; x++ ) {
					if (boxes[x].checked) cnt++; 
				}
				if (cnt == 0 && (!details.match(fields[i].title))) { //check to see if li already in 'details'
					boxli++;
					details += "<li>"+fields[i].title+"</li>";
				}				
			} 
			//email format check - primarily for login/create account/forgot password form
			else if (fields[i].className.match("form-email")) {
				var email = fields[i].value;
				if (isValidEmail(email) == false) {
					details += "<li>The provided email address is not properly formatted; please enter a valid email address (ex. name@hp.com).";	
				}
			}			
			//select drop downs
			else if (fields[i].tagName == 'SELECT' && (fields[i].value == '#' || fields[i].value == '')) { 
				details += "<li>"+fields[i].title+" does not have a selection.</li>";
			} else if ( fields[i].type == 'hidden' ) {
				//skip
			}
		}
	}
	if (details != "<ul>" || formErrors2 != "") {
		details += formErrors2;
		details += '</ul>';
		document.getElementById('validDetails').innerHTML = details;	
		document.getElementById('validSummary').style.display = 'block';
		document.location = "#validSummary";
		return false; 
	} else  {
		//blur_buttons(form); //disabled as seems to cause hickups on ASP pages.
		return true;
	}
}


//blur all buttons on the form
function blur_buttons(form) {
  for (var i=0; i<form.elements.length; i++) {
    if (form.elements[i].type=='button' || form.elements[i].type=='submit' ||
        form.elements[i].type=='reset') {
      form.elements[i].disabled = true;
    }
  }
}

//Get query string from URL.
function getQueryVariable(variable) {
	var query = window.location.search.substring(1);
	var vars = query.split("&");
	for (var i=0;i<vars.length;i++) {
		var pair = vars[i].split("=");
		if (pair[0] == variable) {
			return pair[1];
		}
  	} 
}

//Highlight side nav category. Show/hide 2nd level nav.
// Requires: jQuery
function sidenav(topicID,subtopic) {
  	var tEl = $('#'+topicID);
  	if (tEl.length > 0)
  	{
  		var tParent = tEl.parent("ul.subtopics");
		var tChildren = tEl.children("ul.subtopics");
		tEl.addClass("active");

		if (tParent != undefined)
		{
			tParent.css("display","block");
		}
		if (tChildren.length > 0)
		{
			tChildren.css("display","block");
		}
  	}
	if ( subtopic != '' ) {
  		$("#"+subtopic).addClass("subtopicson");
  	}
}

//Page Tool - Add page to favorites.
function pageToolsBookmark() {
	bkmkurl= document.URL;
	bkmktitle= document.title;
	//if (window.external) {
	if (document.all) {
		window.external.AddFavorite(bkmkurl,bkmktitle);
	} else {
		alert("Please use Control + D to set a bookmark for this page");
	}
}

function pageToolsShare() {
	shareUrl = escape(document.URL);
	shareTitle = escape(document.title);
	shareTag = escape("#digitalmarketing");
	document.write("<a title='Share on HP&rsquo;s WaterCooler' href='http://watercooler.hp.com/chatter/?url="+shareTitle+" - "+shareUrl+" - "+shareTag+"' id='sharethis'>Share</a>");	
}

//Page Tool - Email page to coworkers
function pageToolsEmail() {
	var title = document.title;
	var bar = title.indexOf("|");
	if(bar !== -1)
	{
		title = title.substring(0,bar-1);
	}
	var subj = "You might be interested in this -- "+escape(title);
	var pageurl = document.URL;
	var bodyTxt = "Hi,\n\nI thought you might like to see this page - " + title + ", found at: " + pageurl + ".\n\nThis link will take you to the HP Experience Center and requires you to login.";
	bodyTxt = escape(bodyTxt);
	document.writeln("<a  href='mailto:\?subject=" + subj + "\&body\=" + bodyTxt + "' title=\"Recommend this Web page.\" id=\"emailthis\">Email</a>");
}

// Ultraseek Search functions
function searchsubmit(form) {
	if (form.qt.value=='') {
		alert("Please type in a keyword");
		form.qt.focus();
		return false;
	}
}
function advancedsearchsubmit() {
	var form = document.advancedsearchform;
	if (form.tx0.value=='' && form.tx1.value=='' && form.tx2.value=='' && form.tx3.value=='' ) {
		alert("Please type in a keyword");
		form.tx0.focus();
		return false;
	}
}
function clearSearch(el) {
	if(el.value==sval) {
		el.value='';		
	}
}
function restoreSearch(el){
	if(el.value=='') el.value=sval;
}

//Author: C. Watkins - 08/2003, Updated: 12/2005
function writeIndicator(expires,what) {
	var today = new Date();
	if (today < expires ) {
		document.write("<span class='indicator'>"+what+"</span>");
	}  else {
		//document.write("in the else");
	}
}

//Read XML file and display whats new content
function verify() { 
	if(xmlDoc.readyState != 4) {
		false; 
	}
}

//XML feed reader and display script.
function displayXmlFeed(display,xmlfeed,postcount,pagetype)
{
	var xmlDoc = null;
	var file = "";
	
	if (xmlfeed == "whatsnew")
	{
		file = "/hpweb/experience/ec-whatsnewfeed.xml";
	}
	else if (xmlfeed == "cleansheet")
	{
		file = "/hpweb/blog-cleansheet.xml";
	}
	else if (xmlfeed == "mainblog")
	{
		file = "/hpweb/blog-mainfeed.xml";
	}
	else if (xmlfeed == "mobileblog")
	{
		file = "/hpweb/blog-mobile.xml";
	}
	else
	{
		//no feed to display
	}
	
	//Try to get file for browsers
	try
	{ //IE 
		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async=false;
		xmlDoc.load(file);
	}
	catch(e)
	{
		try
		{ //Firefox, Mozilla, Opera, etc.
			xmlDoc=document.implementation.createDocument("","",null);
			xmlDoc.async=false;
			xmlDoc.load(file);
		}
		catch(e)
		{
			try
			{ //Chrome, Safari
				var xmlhttp = new window.XMLHttpRequest();
				xmlhttp.open("GET",file,false);
				xmlhttp.send(null);
				xmlDoc = xmlhttp.responseXML.documentElement;
			}
			catch(e)
			{
				error=e.message;
			}
		}
	}
	

	if (xmlDoc != null)
	{
		xmlDoc.async = false;
		//xmlDoc.onreadystatechange=verify;
		var xml = xmlDoc.getElementsByTagName("item");
		var lastPubDate = ""; // will be used to track if news items were published on the same date. If so, we don'twrite the date to the screen again.
		
		// Determine if the XML feed has the minimum desired items to display (param: postcount)
		// If not, show the number of items available in the XML feed, otherwise it throws a JS error
		if (xml.length < postcount)
		{
			postcount = xml.length;
		}
		
		if (display == "ul")
		{
			document.write("<ul class='icon-list'>")
			for (var i=0;i<postcount;i++)
			{
				document.write("<li><a href='");
				document.write(xml[i].getElementsByTagName("link")[0].childNodes[0].nodeValue);
				document.write("'>");
				document.write(xml[i].getElementsByTagName("title")[0].childNodes[0].nodeValue);
				if (xmlfeed == "whatsnew")
				{
					document.write("</a></li>");
				}
				else
				{
					document.write("</a></li>");
				}
			}
			document.write("</ul>");
			if (xmlfeed == "whatsnew")
			{
				document.write("<p class='icon arrow'><a href='/hpweb/experience/share/whatsnew.asp'>More recent updates</a></p>");
			}
		}
		else if (display == "dl")
		{
			document.write("<dl class='rssreader'>")
			for (var i=0;i < postcount;i++)
			{ 
				var pubDate = xml[i].getElementsByTagName("pubDate")[0].childNodes[0].nodeValue
				pubDate = pubDate.substring(5,17);
				var description = xml[i].getElementsByTagName("description")[0].childNodes[0].nodeValue;
				description = description.replace("[...]","&#8230;");
				
				document.write("<dt>");
				if (pagetype == "homepage")
				{
					document.write("<span class=\"info\">" + pubDate + "</span>");
				}
				document.write("<strong>");
				document.write("<a href=\"");
				document.write(xml[i].getElementsByTagName("link")[0].childNodes[0].nodeValue);
				document.write("\">");
				document.write(xml[i].getElementsByTagName("title")[0].childNodes[0].nodeValue);
				document.write("</a></strong></dt>");	
				document.write("<dd>");
				document.write(description);
				document.write("</dd>");
			}
			if (xmlfeed == "mainblog" && pagetype == "homepage")
			{
				document.write("<dt>&raquo; Read more: <a href='http://blogs.hp.com/hpdigitalcommunity/'>our blog</a> or <a href='/hpweb/news/index.asp'>news</a></dt>");
			}
			document.write("</dl>");
			if (xmlfeed == "whatsnew" && pagetype !== "homepage")
			{
				document.write("<p class='icon arrow'><a href='/hpweb/experience/share/whatsnew.asp'>More recent updates</a></p>");
			}
		}
		else if (display == "fancy")
		{
			for (var i=0;i < postcount;i++)
			{
				var pubDate = xml[i].getElementsByTagName("pubDate")[0].childNodes[0].nodeValue;
				pubDate = pubDate.substring(5,17);
				
				// Determine the next publishing date (if available)
				// Used to identify if we can close the .news-of-day DIV
				if (xml[i+1])
				{
					var nextPubDate = xml[i+1].getElementsByTagName("pubDate")[0].childNodes[0].nodeValue;
					nextPubDate = nextPubDate.substring(5,17);
				}
				else
				{
					nextPubDate = null;
				}
				
				var description = xml[i].getElementsByTagName("description")[0].childNodes[0].nodeValue;
				description = description.replace("[...]","&#8230;");
				
				var isNewNewsItem = false;
				var isNewNewsDay = false;
				var today = new Date();
				var todayInMS = today.getTime(); // Today in milliseconds since Jan. 1, 1970
				var pubDateInMS = Date.parse(pubDate); // Publish date in milliseconds since Jan. 1, 1970
				var newDurationInMS = 86400000*10; // The "new" duration of time in milliseconds (milliseconds per day x number of days)
				
				// Determine if the news item falls within the "new" window and should receive the "new" indicator
				if (todayInMS - pubDateInMS <= newDurationInMS)
				{
					isNewNewsItem = true;
				}
				
				// Determine if the news item was published on a different date from the previous one.
				if (pubDate !== lastPubDate)
				{
					isNewNewsDay = true;
					lastPubDate = pubDate;
				}
				
				if (isNewNewsDay === true)
				{
					document.write('<hr class="section-break-medium">');
					document.write('<div class="news-of-day">');
				}
				
				document.write('<div class="news-item">');
				document.write('<div class="news-item-data">');
				document.write('<span class="date">');
				
				if (isNewNewsDay === true)
				{
					document.write(pubDate);
				}
				
				document.write('</span>');
				
				// Check to see if this item falls within the "new" window and display the new indicator accordingly
				if (isNewNewsItem === true)
				{
					document.write('<span class="indicator">New</span>');
				}
				
				document.write('</div>'); // close .news-item-data
				document.write('<div class="news-item-content">');
				document.write('<h4>');
				document.write('<a href="');
				document.write(xml[i].getElementsByTagName('link')[0].childNodes[0].nodeValue);
				document.write('">');
				document.write(xml[i].getElementsByTagName('title')[0].childNodes[0].nodeValue);
				document.write('</a>');
				document.write('</h4>');
				document.write('<p>');
				document.write(description);
				document.write('</p>');
				document.write('</div>'); // close .news-item-content
				document.write('</div>'); // close .news-item
				
				if (nextPubDate !== pubDate || nextPubDate === null)
				{
					document.write('</div>'); // close .news-of-day
				}
			}
		}
		else
		{
			//no other format	
		}
	}
}