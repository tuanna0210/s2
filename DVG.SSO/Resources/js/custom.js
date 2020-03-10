var requestUrl;
var timeout;

function SetFileField(fileUrl, data) {
    document.getElementById(data['selectActionData']).value = fileUrl;
}
function ShowImage(id) {
    $("#screenshot").remove();
    $("#tooltipImageSrc").append("<p id='screenshot'><img src='" + $('#' + id).val() + "' alt='url preview' />" + "" + "</p>");
    $("#screenshot").fadeIn("fast");
}
function HideImage() {
    $("#screenshot").remove();
}
function ShowQuickTool(id) {
    $('.quickTool').removeClass('show');
    $('#' + id).addClass('show')
}
$(document).ready(function () {
    $('button[type=submit]').click(function () {
        var form = $(this).parents('form:first');
        if (form.valid()) {
            form.submit(function (e) {
                var data = form.serialize();
                if (this.beenSubmitted == data)
                    return false;
                else
                    this.beenSubmitted = data;
                    $.post(form.attr('action'), data, function (response) {
                    if (response.Error) {
                        CreateMessageError(response.Title);
                    }
                    else {
                        CreateMessageSucces(response.Title);
                    }
                });
                return false;
            });
        }
    });
});
function Search(url, keyword) {
    $('#txtSearch').val(url + keyword);
}
$(document).keypress(function (event) {
    if ((event.keyCode ? event.keyCode : event.which) == '13') {
        LoadListData(encodeURI($('#txtSearch').val()));
    }
});
function GoSearch()
{
    LoadListData(encodeURI($('#txtSearch').val()));
}
function LoadListData(url) {
    var obj = document.getElementById('ajaxContainer');
    obj.Empty;
    var img = document.createElement("IMG");
    img.src = "/Resources/images/loaders/7.gif";
    obj.appendChild(img);
    $(obj).load(url, function () {
        $('table > tbody > tr > td > ul > li > a > i').on("click", function () {
            Action($(this).closest('a').attr("hreflang"), $(this).attr("class"), $(this).closest('tr').attr('id'));
        });
        $('.quickTool > a > i').on("click", function () {
            Action($(this).closest('a').attr("hreflang"), $(this).attr("class"), $(this).closest('li').attr('id'));
        });
    });
}
function LoadListData2(url, tagid) {
    var obj = document.getElementById(tagid);
    obj.Empty;
    var img = document.createElement("IMG");
    img.src = "/Resources/images/loaders/7.gif";
    obj.appendChild(img);
    $(obj).load(url, function () {
        $('table > tbody > tr > td > ul > li > a > i').on("click", function () {
            Action($(this).closest('a').attr("hreflang"), $(this).attr("class"), $(this).closest('tr').attr('id'));
        });
    });
}
function GoPage(Controler, CatId, Status, FieldSort, FieldOption, RowPerPage, CurrentPage, Keyword, SearchIn) {
    url = 'mList/?' + 'CatId=' + CatId + '&Status=' + Status + '&FieldSort=' + FieldSort + '&FieldOption=' + FieldOption + '&RowPerPage=' + RowPerPage + '&CurrentPage=' + CurrentPage + '&Keyword=' + encodeURIComponent(Keyword) + '&SearchIn=' + SearchIn;
    LoadListData(url);
}

//Thêm sửa xóa

function ShowQuickTool(id) {
    $('.quickTool').removeClass('show');
    $('#' + id).addClass('show')
}
function View(controler,title, id) {
    LoadControler('dialog', '/' + controler + '/mView/' + id);
    $('#dialog').dialog('open');
    $('#ui-dialog-title-dialog').html("Xem thông tin " + title);
    return false;
}
function EditNew(controler, title, id) {
    LoadControler('dialog', '/' + controler + '/mForm/' + id);
    $('#dialog').dialog('open');
    $('#ui-dialog-title-dialog').html("Sửa " + title);
    return false;
}
function Action(remotecontroler, action, id)
{
    switch (action) {
        case 'fam-pencil':
            document.location.href = 'mform/' + id;
            break;
        case 'fam-add':
            document.location.href = 'mform/?ParentId=' + id;
            break;
        case 'fam-comment-add':
            document.location.href = '/thetest/answer/mform?ParentId=' + id;
            break;
        case 'fam-page-add':
            PostUrlMesageAddTest(remotecontroler, remotecontroler + 'mformaddtmp/' + id);
            break;
        case 'fam-page-delete':
            PostUrlMesageRemoveTest(remotecontroler, remotecontroler + 'mformremovetmp/' + id);
            break;
        case 'fam-page-edit':
            document.location.href = '/thetest/home/mformaddquestion?ParentId=' + id;
            break;
        case 'fam-comments-add':
            document.location.href = '/thetest/examination/mformaddtest?ParentId=' + id;
            break;
        case 'fam-user-add':
            document.location.href = 'mFormAddUser/' + id;
            break;
        case 'fam-cross':
            $("#" + id).fadeOut();
            PostUrlMesage('delete/' + id);
            break;
        case 'fam-application-view-detail':
            document.location.href = 'mview/' + id + '?' + remotecontroler;
            break;
        case 'fam-table-sort':
            document.location.href = 'mSort/' + id;
            break;
        case 'fam-page-go':
            PostUrlMesage('ChangeStatus/' + id + '?status=2');
            $("#" + id).fadeOut();
            break;
        case 'fam-tick':
            PostUrlMesage('ChangeStatus/' + id + '?status=3');
            $("#" + id).fadeOut();
            break;
        case 'fam-arrow-down':
            PostUrlMesage('ChangeStatus/' + id + '?status=4');
            $("#" + id).fadeOut();
            break;
        case 'icon-trash':
            PostUrlMesage('ChangeStatus/' + id + '?status=5');
            $("#" + id).fadeOut();
            break;
        case 'fam-arrow-redo':
            PostUrlMesage('ChangeStatus/' + id + '?status=1');
            $("#" + id).fadeOut();
            break;
        case 'fam-key-go':
            PostUrlMesage('mReSetPass/' + id);
            break;
        default:
            document.location.href = 'mview/' + id;
    }
}

function Sort(controler, title, id) {
    LoadControler('dialog', '/' + controler + '/mSort');
    $('#dialog').dialog('open');
    $('#ui-dialog-title-dialog').html("Sắp xếp " + title);
    return false;
}
function ViewComment(controler, title, id) {
    LoadControler('dialog', '/' + controler + '/mListComment/' + id);
    $('#dialog').dialog('open');
    $('#ui-dialog-title-dialog').html("Tạo mới " + title);
    return false;
}
function Hide(controler, title, id) {
    PostUrlMesage("/" + controler + "/ShowHide/" + id);
}
//End Thêm sửa xóa

$(function () {
    $('.tooltip').tooltip({
        track: true,
        delay: 0,
        showURL: false,
        showBody: " - ",
        fade: 250
    });
});
function PostUrl(url) {
    $.ajax({
        type: "POST", url: url,
        success: function (html) {
        }
    });
}
function PostUrlAnswer(url) {
    $.ajax({
        type: "POST", url: url,
        success: function (response) {
            if (response.Error) {
                CreateMessageError(response.Title);
            }
            else {
                $("#lstAnswer").prepend("<div class=\"alert alert-info\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + response.Obj + response.Title + "</div>");
            }
        }
    });
}
function PostUrlMesage(url) {
    $.ajax({
        type: "POST", url: url,
        success: function (response) {
            if (response.Error) {
                CreateMessageError(response.Title);
            }
            else { CreateMessageSucces(response.Title); }
        }
    });
}
function PostUrlMesageAddTest(remotecontroler, url) {
    
    url += "?currentSelected=" + $("#lstSelected").val();
    if (requestUrl != url)
    {
        requestUrl = url;
        request = $.ajax({
            type: "POST", url: url,
            success: function (response) {
                if (response.Error) {
                    CreateMessageError(response.Title);
                }
                else {
                    CreateMessageSucces(response.Title);
                    $("#lstSelected").val(response.Obj);
                    LoadListData2(remotecontroler + 'mlistselected/?lstSelected=' + response.Obj, 'ajaxTest');
                }
            }
        });
    }
}
function PostUrlMesageRemoveTest(remotecontroler, url) {
    url += "?currentSelected=" + $("#lstSelected").val();
    if (requestUrl != url) {
        requestUrl = url;
        $.ajax({
            type: "POST", url: url,
            success: function (response) {
                if (response.Error) {
                    CreateMessageError(response.Title);
                }
                else {
                    CreateMessageSucces(response.Title);
                    $("#lstSelected").val(response.Obj);
                    LoadListData2(remotecontroler + 'mlistselected/?lstSelected=' + response.Obj, 'ajaxTest');
                }
            }
        });
    }
}
function PostUrlReturnData(url, objId) {
    $.ajax({
        type: "POST", url: url,
        success: function (data) {
            $("#" + objId+ "").val(data);
        }
    });
    return true;
}
function PostUrlMesage2(container, urlPost, urlReturn) {
    $.ajax({
        type: "POST", url: urlPost,
        success: function (response) {
            if (response.Error) {
                document.getElementById('lblMessagePopup').innerHTML = "<div class=\"response-msg error ui-corner-all\"><span>Error message</span>" + response.Title + "</div>";
            }
            else {
                LoadControler(container, urlReturn);
                document.getElementById('lblMessagePopup').innerHTML = "<div class=\"response-msg success ui-corner-all\"><span>Success message</span>" + response.Title + "</div>";
            }
        }
    });
}
function ReloadConten()
{
    url = "/Shared/History";
    $.ajax({
        type: "POST", url: url,
        success: function (html) {
            if(html!="0")
                LoadControler('container',html);
        }
    });
}
function ReloadConten1(container, url) {
    $.ajax({
        type: "GET", url: url,
        success: function (html) {
            if (html != "0")
                LoadControler(container, html);
        }
    });
}
//Mesage
function CreateMessageSuccesNoReload(strMessage) {
    $.jGrowl(strMessage, { sticky: true, theme: 'growl-success', header: 'Thành công!' });
}
function CreateMessageSucces(strMessage) {
    $.jGrowl(strMessage, { sticky: true, theme: 'growl-success', header: 'Thành công!' });
}
function CreateMessageError(strMessage) {
    $.jGrowl(strMessage, { sticky: true, theme: 'growl-error', header: 'Lỗi!' });
}

function CreateMessageSucces2(strMessage, container, url) {
    //document.getElementById('lblMessagePopup').innerHTML = "<div class=\"response-msg success ui-corner-all\"><span>Success message</span>" + strMessage + "</div>";
    //ReloadConten(container, url);
   // LoadControler(container, url);
}
function CreateMessageError2(strMessage, container) {
    //document.getElementById('#' + container).innerHTML = "<div class=\"response-msg error ui-corner-all\"><span>Error message</span>" + strMessage + "</div>";
}

function LoadControler(divid, pathcontroler) {
    $('#' + divid).Empty;
    var img = document.createElement("IMG");
    img.src = "/Resources/images/table/loaders/9.gif";
    document.getElementById(divid).appendChild(img);
    document.getElementById(divid).appendChild(img);

    $.get(pathcontroler, function (data) {
        $('#' + divid).html(data);
    });
    //$('#' + divid).load(pathcontroler);
}
function Loading() {
    document.getElementById('container').innerHTML = 'Loading ...';
}
$(document).ready(function () {
    //$("ul.side-menu li a").click(function () {
    //    var list_a = $("ul.side-menu li").find('a');
    //    for (var i = 0; i < list_a.length; i++) {
    //        $(list_a[i]).css({ 'color': '#222222', 'font-weight': 'normal' });
    //    }
    //    $(this).css({ 'color': '#E17009', 'font-weight': 'bold' });
    //});
    // khởi tạo giá trị
    //$('#totalUser').load('/shared/TotalUser');
    //$('#totalRole').load('/shared/TotalRole');
    //$('#totalGroupRole').load('/shared/TotalGroupRole');
    //$('#totalTypeArticle').load('/shared/TotalTypearticle');
    //$('#totalArticlepublic').load('/shared/TotalArticle/3');
    //$('#totalWaitPublich').load('/shared/TotalArticle/2');
    //$('#totalArticledraf').load('/shared/TotalArticle/1');
    //$('#totalArticledown').load('/shared/TotalArticle/4');
    //$('#totalArticlerecyclebin').load('/shared/TotalArticle/5');
    //$('#totalCategories').load('/shared/TotalCategory');

    //$('#totalSurveyDraf').load('/shared/TotalSurvey/1');
    //$('#totalSurveyWaitPublich').load('/shared/TotalSurvey/2');
    //$('#totalSurveyPublich').load('/shared/TotalSurvey/3');
    //$('#totalSurveyDow').load('/shared/TotalSurvey/4');
    //$('#totalSurveyDelete').load('/shared/TotalSurvey/5');

    //$('#totalPositionAds').load('/shared/TotalAdsPosition');
    //$('#totalAds').load('/shared/TotalAds');
    //$('#totalTags').load('/shared/totalTags');

    //$('#totalAlbumDraf').load('/shared/TotalAlbum/1');
    //$('#totalAlbumPublish').load('/shared/TotalAlbum/3');
    //$('#totalImage').load('/shared/TotalImage');
});//kết thúc readyfunction


/* Check all table rows */

var checkflag = "false";
function check(field) {
    if (checkflag == "false") {
        for (i = 0; i < field.length; i++) {
            field[i].checked = true;
        }
        checkflag = "true";
        return "check_all";
    }
    else {
        for (i = 0; i < field.length; i++) {
            field[i].checked = false;
        }
        checkflag = "false";
        return "check_none";
    }
}

checked = false;
function checkedAll(frm1) {
    var aa = document.getElementById('UserList');
    if (checked == false) {
        checked = true;
    }
    else {
        checked = false;
    }
    for (var i = 0; i < aa.elements.length; i++) {
        aa.elements[i].checked = checked;
    }
}

