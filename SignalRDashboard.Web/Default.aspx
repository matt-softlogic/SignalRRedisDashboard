<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SignalRDashboard.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>SignalR Dashboard</title>

    <script src="Scripts/jquery-1.9.0.min.js"></script>
    <script src="Scripts/jquery.signalR-2.1.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="signalr/hubs"></script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">Dashboard Sample</a>
                </div>
            </div>
        </div>
        <div class="container body-content">
            <h2><%: Title %></h2>
            <hr />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title"><span class="glyphicon glyphicon-dashboard"></span>&nbsp;Dashboard Sample</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><span class="glyphicon glyphicon-user"></span>&nbsp;Client ID</h3>
                                </div>
                                <div class="panel-body">
                                    <label id="clientIdLabel">Unknown</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><span class="glyphicon glyphicon-pencil"></span>&nbsp;Dev</h3>
                                </div>
                                <div class="panel-body">
                                    <button id="DEV1" type="button" class="btn btn-lg" disabled="disabled">Dev 1 (00)</button>
                                    <button id="DEV2" type="button" class="btn btn-lg" disabled="disabled">Dev 2 (00)</button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><span class="glyphicon glyphicon-comment"></span>&nbsp;UAT</h3>
                                </div>
                                <div class="panel-body">
                                    <button id="UAT1" type="button" class="btn btn-lg" disabled="disabled">UAT 1 (00)</button>
                                    <button id="UAT2" type="button" class="btn btn-lg" disabled="disabled">UAT 2 (00)</button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><span class="glyphicon glyphicon-cog"></span>&nbsp;PRD</h3>
                                </div>
                                <div class="panel-body">
                                    <button id="PRD1" type="button" class="btn btn-lg" disabled="disabled">PRD 1 (00)</button>
                                    <button id="PRD2" type="button" class="btn btn-lg" disabled="disabled">PRD 2 (00)</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - Dashboard Sample</p>
            </footer>
        </div>
        <script>
            $(function () {
                // Reference the auto-generated proxy for the hub.
                var hub = $.connection.statusHub;

                // Create a function that the hub can call back to display messages.
                hub.client.processStatusMessage = function (systemName, value, status) {
                    // get appropriate class from status
                    var c = "btn btn-lg";

                    if (status == "Critical") {
                        c = "btn btn-lg btn-danger";
                    }

                    if (status == "Warning") {
                        c = "btn btn-lg btn-warning";
                    }

                    if (status == "Normal") {
                        c = "btn btn-lg btn-success";
                    }

                    // assign the class to the appropriate button
                    $("#" + systemName).attr("class", c);

                    // assign name based on value
                    $("#" + systemName).text(systemName + " (" + value + ")")
                }

                // Start the connection.
                $.connection.hub.start().done(function () {
                    // obtain and display the client ID
                    var clientId = $.connection.hub.id;
                    $("#clientIdLabel").text(clientId);
                });
            });
        </script>
    </form>
</body>
</html>
