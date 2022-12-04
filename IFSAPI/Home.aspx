<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IFSAPI.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API for IFS</title>
    <style>
        .collapsible {
            background-color: #777;
            color: white;
            cursor: pointer;
            padding: 18px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 15px;
        }

            .active, .collapsible:hover {
                background-color: #555;
            }

        .content {
            padding: 0 18px;
            display: none;
            overflow: hidden;
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-6">

            <button type="button" class="collapsible">General Error</button>
            <div class="content">
                <asp:Label ID="lblGeneralError" Text="" runat="server" />
            </div>

            <button type="button" class="collapsible">Normal Invoices (BLL-GTM)</button>
            <div class="content">
                <asp:Label ID="lbl_normalinvoice" Text="" runat="server" />
            </div>
            <button type="button" class="collapsible">Trade Promotion (Zero Value Invoices)</button>
            <div class="content">
                <asp:Label ID="lbl_tradepromotion" Text="" runat="server" />
            </div>
            <button type="button" class="collapsible">Replacement Issued</button>
            <div class="content">
                <asp:Label ID="lbl_replaceissue" Text="" runat="server" />
            </div>
            <button type="button" class="collapsible">Breakage</button>
            <div class="content">
                <asp:Label ID="lbl_breakage" Text="" runat="server" />
            </div>

            <button type="button" class="collapsible">Normal Invoices (TD)</button>
            <div class="content">
                <asp:Label ID="lbl_tdnormalinvoice" Text="" runat="server" />
            </div>

            <button type="button" class="collapsible">Primary Sales Order (BLL)</button>
            <div class="content">
                <asp:Label ID="lbl_SalesOrder" Text="" runat="server" />
            </div>

            <button type="button" class="collapsible">Replacement Claim</button>
            <div class="content">
                <asp:Label ID="lbl_Replaceclaim" Text="" runat="server" />
            </div>
            <button type="button" class="collapsible">Normal Invoices (Lighting Project)</button>
            <div class="content">
                <asp:Label ID="lbl_LightingInvoices" Text="" runat="server" />
            </div>
        </div>
        <div class="col-lg-6">
            <button type="button" class="collapsible">Error Log</button>
            <div class="content">
                <asp:Label ID="Lbl_errorlist" Text="" runat="server" />
            </div>
        </div>
    </form>

    <script>
        var coll = document.getElementsByClassName("collapsible");
        var i;

        for (i = 0; i < coll.length; i++) {
            coll[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var content = this.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        }
    </script>
</body>
</html>
