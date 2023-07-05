﻿@Code
	ViewBag.Title = "Start uploading manually"
End Code
<h2>
    Start uploading manually</h2>
<p>
    This sample demonstrates how to start uploading manually after file selection vs
    automatically.</p>			
@Code Html.BeginForm() End Code
	@Html.Raw(ViewBag.uploaderhtml)
<br />
<br />
<button id="SubmitButton" onclick="return submitbutton_click()">Submit</button>			
@Code Html.EndForm() End Code
<br />
<br />
<div>
    <textarea id="text_info" style="height:200px; width:400px;"></textarea>
</div>

<script type="text/javascript">
    function submitbutton_click() {
        var submitbutton = document.getElementById('SubmitButton');
        var uploadobj = document.getElementById('myuploader');
        if (!window.filesuploaded) {
        	if (uploadobj.getqueuecount() > 0) {
        		uploadobj.startupload();
        	}
        	else {
        		var uploadedcount = parseInt(submitbutton.getAttribute("itemcount")) || 0;
        		if (uploadedcount > 0) {
        			return true;
        		}
        		alert("Please browse files for upload");
        	}
        	return false;
        }
        window.filesuploaded = false;
        return true;
    }
    function CuteWebUI_AjaxUploader_OnPostback() {
        window.filesuploaded = true;
        var submitbutton = document.getElementById('SubmitButton');
        submitbutton.click();
        return false;
    }
    function GetTrace() {
        var html = '@Convert.ToString(ViewBag.UploadedMessage)' || '';
        if (html) {
        	document.getElementById("text_info").value = html;
        }
    }
    GetTrace();
</script>