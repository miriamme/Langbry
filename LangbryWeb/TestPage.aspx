<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="WebTest.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--include jquery library--%>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
    <%--web.config에 설정된 js 경로 추가--%>
    <script src="langbry_app.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <select id="langsel">
                    <option value="">---선택---</option>
                    <option value="EN">English</option>
                    <option value="KO">한글</option>
                </select>
            </div>
            <div>
                <langbry>1</langbry>
            </div>
            <div>
                <xl-lang>2</xl-lang>
            </div>
            <div>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblGetCode" runat="server"></asp:Label>
            </div>
        </div>
    </form>

    <script>
		$(function () {
			$("#langsel").on("change", function (e) {
				console.log(e.target.value);
				console.log(getCookie("language_code"));
				if (e.target.value !== "") {
					setCookie("<%=base.LangCookieName%>", e.target.value, 10);
					document.location.reload();
				}
			});
		});

		function getCookie(cname) {
			var name = cname + "=";
			var decodedCookie = decodeURIComponent(document.cookie);
			var ca = decodedCookie.split(';');
			for (var i = 0; i < ca.length; i++) {
				var c = ca[i];
				while (c.charAt(0) == ' ') {
					c = c.substring(1);
				}
				if (c.indexOf(name) == 0) {
					return c.substring(name.length, c.length);
				}
			}
			return "";
		}

		function setCookie(cname, cvalue, exdays) {
			var d = new Date();
			d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
			var expires = "expires=" + d.toUTCString();
			document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
		}
    </script>
</body>
</html>