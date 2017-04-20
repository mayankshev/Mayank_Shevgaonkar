<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>view</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            color: #0000FF;
        }
        .auto-style3 {
            color: #FF0000;
        }
    </style>
    <script src="../../Scripts/jquery-3.1.1.js"></script>
    <script>
        function validate() {
            var x = (document.forms["myForm"]["code"].value + "" + document.forms["myForm"]["mobile"].value);
            var c = document.getElementById('sp');
            if (x != " ") {
                c.innerText = ("Number will be stored as  :  " + x);
                //alert("Number is Successfuly submitted:  "+ x);
                return false;
            }
        }
    </script>
</head>
<script src="../../Scripts/angular.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js"></script> 
<body data-ng-app="">
    <div>
        <h1 style="text-align:center" class="auto-style2">Enter Number</h1>
        <form action="/" method="post" name="myForm" onsubmit="return validate()">

            <table class="auto-style1" style="text-align:center">
                <tr>
                    <td>Code:</td>
                    <td>Mobile Number:</td>
                </tr>
                <tr>
                    <td><input type="text" 
                         name="code"
                         maxlength="4"
                         placeholder="+91"                           
                         data-ng-model="code" 
                         required="required"
                         data-ng-pattern=""/>

                    </td>
                    <td><input type="text"
                         name="mobile"
                         maxlength="10"
                         value="" required="required"
                         data-ng-model="mobile"
                         data-ng-pattern="/^(\+\d{1,3}[- ]?)?\d{10}$/" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <span data-ng-show="myForm.code.$touched && myForm.code.$invalid" 
                            class="auto-style3">Code can't be blank </span>

                    </td>
                    <td>
                        <span data-ng-show="myForm.mobile.$touched && myForm.mobile.$invalid" 
                            class="auto-style3">Mobile Number can't be blank</span>
                    </td>
    
                </tr>
                <tr>
                   <td style="text-align:center"><input type="submit" name="sub" value="Submit" style="height: 34px" /></td>
                </tr>                                
            </table>
            <br /><br /><br /><span id="sp" style="text-align:center"></span>
        </form>
    </div>
</body>
</html>
