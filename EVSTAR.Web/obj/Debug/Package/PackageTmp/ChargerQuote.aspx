<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_SCA.Master" AutoEventWireup="true" CodeBehind="ChargerQuote.aspx.cs"
    Inherits="EVSTAR.Web.ChargerQuote" MaintainScrollPositionOnPostback="true" ClientIDMode="Static" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel CssClass="login-block-desktop" runat="server">
        <asp:Panel runat="server" CssClass="login-content-desktop" ID="divDesktop">
            <div class="login-content-highlights-desktop">
                Request a Quote.
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">First Name *
                        </span>
                        <br />
                        <asp:TextBox runat="server" ID="txtFirstName" MaxLength="50" CssClass="login-text-input-desktop" placeholder="First Name" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Last Name *
                        </span>
                        <br />
                        <asp:TextBox runat="server" ID="txtLastName" MaxLength="50" CssClass="login-text-input-desktop" placeholder="Last Name" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">&nbsp;
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Phone Number *<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtPhoneNumber" MaxLength="15" CssClass="login-text-input-desktop" placeholder="Phone Number" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Email *<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtEmail" MaxLength="128" CssClass="login-text-input-desktop" placeholder="Email Address" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">&nbsp;
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Street Address *<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtStreetAddress1" MaxLength="50" CssClass="login-text-input-desktop" placeholder="Street Address" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Street Address Line 2<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtStreetAddress2" MaxLength="50" CssClass="login-text-input-desktop" placeholder="Street Address Line 2" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">&nbsp;
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">City *<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtCity" MaxLength="50" CssClass="login-text-input-desktop" placeholder="City" Width="300"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">State *<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlState" Width="300">
                            <asp:ListItem Value="AL" Text="Alabama"></asp:ListItem>
                            <asp:ListItem Value="AK" Text="Alaska"></asp:ListItem>
                            <asp:ListItem Value="AZ" Text="Arizona"></asp:ListItem>
                            <asp:ListItem Value="AR" Text="Arkansas"></asp:ListItem>
                            <asp:ListItem Value="CA" Text="California"></asp:ListItem>
                            <asp:ListItem Value="CO" Text="Colorado"></asp:ListItem>
                            <asp:ListItem Value="CT" Text="Connecticut"></asp:ListItem>
                            <asp:ListItem Value="DE" Text="Delaware"></asp:ListItem>
                            <asp:ListItem Value="DC" Text="District Of Columbia"></asp:ListItem>
                            <asp:ListItem Value="FL" Text="Florida"></asp:ListItem>
                            <asp:ListItem Value="GA" Text="Georgia"></asp:ListItem>
                            <asp:ListItem Value="HI" Text="Hawaii"></asp:ListItem>
                            <asp:ListItem Value="ID" Text="Idaho"></asp:ListItem>
                            <asp:ListItem Value="IL" Text="Illinois"></asp:ListItem>
                            <asp:ListItem Value="IN" Text="Indiana"></asp:ListItem>
                            <asp:ListItem Value="IA" Text="Iowa"></asp:ListItem>
                            <asp:ListItem Value="KS" Text="Kansas"></asp:ListItem>
                            <asp:ListItem Value="KY" Text="Kentucky"></asp:ListItem>
                            <asp:ListItem Value="LA" Text="Louisiana"></asp:ListItem>
                            <asp:ListItem Value="ME" Text="Maine"></asp:ListItem>
                            <asp:ListItem Value="MD" Text="Maryland"></asp:ListItem>
                            <asp:ListItem Value="MA" Text="Massachusetts"></asp:ListItem>
                            <asp:ListItem Value="MI" Text="Michigan"></asp:ListItem>
                            <asp:ListItem Value="MN" Text="Minnesota"></asp:ListItem>
                            <asp:ListItem Value="MS" Text="Mississippi"></asp:ListItem>
                            <asp:ListItem Value="MO" Text="Missouri"></asp:ListItem>
                            <asp:ListItem Value="MT" Text="Montana"></asp:ListItem>
                            <asp:ListItem Value="NE" Text="Nebraska"></asp:ListItem>
                            <asp:ListItem Value="NV" Text="Nevada"></asp:ListItem>
                            <asp:ListItem Value="NH" Text="New Hampshire"></asp:ListItem>
                            <asp:ListItem Value="NJ" Text="New Jersey"></asp:ListItem>
                            <asp:ListItem Value="NM" Text="New Mexico"></asp:ListItem>
                            <asp:ListItem Value="NY" Text="New York"></asp:ListItem>
                            <asp:ListItem Value="NC" Text="North Carolina"></asp:ListItem>
                            <asp:ListItem Value="ND" Text="North Dakota"></asp:ListItem>
                            <asp:ListItem Value="OH" Text="Ohio"></asp:ListItem>
                            <asp:ListItem Value="OK" Text="Oklahoma"></asp:ListItem>
                            <asp:ListItem Value="OR" Text="Oregon"></asp:ListItem>
                            <asp:ListItem Value="PA" Text="Pennsylvania"></asp:ListItem>
                            <asp:ListItem Value="RI" Text="Rhode Island"></asp:ListItem>
                            <asp:ListItem Value="SC" Text="South Carolina"></asp:ListItem>
                            <asp:ListItem Value="SD" Text="South Dakota"></asp:ListItem>
                            <asp:ListItem Value="TN" Text="Tennessee"></asp:ListItem>
                            <asp:ListItem Value="TX" Text="Texas"></asp:ListItem>
                            <asp:ListItem Value="UT" Text="Utah"></asp:ListItem>
                            <asp:ListItem Value="VT" Text="Vermont"></asp:ListItem>
                            <asp:ListItem Value="VA" Text="Virginia"></asp:ListItem>
                            <asp:ListItem Value="WA" Text="Washington"></asp:ListItem>
                            <asp:ListItem Value="WV" Text="West Virginia"></asp:ListItem>
                            <asp:ListItem Value="WI" Text="Wisconsin"></asp:ListItem>
                            <asp:ListItem Value="WY" Text="Wyoming"></asp:ListItem>
                            <asp:ListItem Value="AS" Text="American Samoa"></asp:ListItem>
                            <asp:ListItem Value="GU" Text="Guam"></asp:ListItem>
                            <asp:ListItem Value="MP" Text="Northern Mariana Islands"></asp:ListItem>
                            <asp:ListItem Value="PR" Text="Puerto Rico<"></asp:ListItem>
                            <asp:ListItem Value="UM" Text="United States Minor Outlying Islands"></asp:ListItem>
                            <asp:ListItem Value="VI" Text="Virgin Islands"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">ZIP Code *<br />
                        </span>
                        <asp:TextBox runat="server" ID="txtPostalCode" MaxLength="10" CssClass="login-text-input-desktop" placeholder="ZIP Code" Width="300"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm" style="font-size: 1.2em;">
                        Vehicle Description<br />
                    </div>
                    <div class="col-sm">&nbsp;</div>
                    <div class="col-sm">&nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Car Year<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlCarYear" Width="300">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Car Make<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlCarMake" Width="300">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Car Model<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="dropdown-input-desktop" ID="ddlCarModel" Width="300">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <asp:Panel runat="server" ID="divNonTesla" Style="display: none;">
                            <span class="login-text-input-label-desktop" style="width: 300px;">Do you have a Car Charger?<br />
                            </span>
                            <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHaveCarCharger" Width="300">
                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                            </asp:DropDownList>
                            <div id="divNonTeslaYes" class="login-text-input-label-desktop" style="width: 300px; display: none;">
                                Please let us know the make and model of car charger below in Additional Information.
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="divTesla">
                            <span class="login-text-input-label-desktop" style="width: 300px;">Preferred Charging Station *<br />
                            </span>
                            <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlPreferredCharger" Width="300">
                                <asp:ListItem Value="NotSure" Text="Not Sure"></asp:ListItem>
                                <asp:ListItem Value="Tesla" Text="Tesla Wall Connector"></asp:ListItem>
                                <asp:ListItem Value="240V" Text="240v Outlet"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                    <div class="col-sm">
                        Electric Car Date of Delivery<br />
                        <%--                        <asp:Calendar runat="server" ID="calCarDeliveryDate" CssClass="login-text-input-desktop" Width="300"></asp:Calendar>--%>
                        <input type="text" id="calCarDeliveryDate" class="dropdown-input-desktop" style="width: 300px;" />
                    </div>
                    <div class="col-sm">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Dealership Purchased From<br />
                        </span>
                        <asp:TextBox ID="txtDealership" runat="server" MaxLength="50" Width="300" CssClass="login-text-input-desktop" placeholder="Dealership Purchased From"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Dealership Sales Agent (optional)<br />
                        </span>
                        <asp:TextBox ID="txtDealershipAgent" runat="server" MaxLength="50" Width="300" CssClass="login-text-input-desktop" placeholder="Dealership Sales Agent"></asp:TextBox>
                    </div>
                    <div class="col-sm">&nbsp;</div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm" style="font-size: 1.2em;">
                        Home Description
                    </div>
                    <div class="col-sm">&nbsp;</div>
                    <div class="col-sm">&nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Home Square Feet<br />
                        </span>
                        <asp:TextBox ID="txtHomeSquareFeet" runat="server" MaxLength="5" Width="300" CssClass="login-text-input-desktop" placeholder="Home Square Feet"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Attic Access<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlAtticAccess" Width="300">
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">&nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Basement?<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHasBasement" Width="300">
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            <asp:ListItem Value="Finished" Text="Yes - Finished"></asp:ListItem>
                            <asp:ListItem Value="Unfinished" Text="Yes - Unfinished"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Electric Utility Company<br />
                        </span>
                        <asp:TextBox ID="txtElectricCompany" runat="server" MaxLength="50" Width="300" CssClass="login-text-input-desktop" placeholder="Electric Utility Company"></asp:TextBox>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Gate Code (Community or Private)<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHasGateCode" Width="300">
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Panel runat="server" ID="divGateCode" Style="display: none;">
                            <span class="login-text-input-label-desktop" style="width: 300px;">Code:<br />
                            </span>
                            <asp:TextBox ID="txtGateCode" runat="server" MaxLength="50" Width="300" CssClass="login-text-input-desktop" placeholder="Gate Code"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Parking<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlParking" Width="300">
                            <asp:ListItem Value="One Car Garage" Text="One Car Garage"></asp:ListItem>
                            <asp:ListItem Value="Two Car Garage" Text="Two Car Garage"></asp:ListItem>
                            <asp:ListItem Value="Three Car Garage" Text="Three Car Garage"></asp:ListItem>
                            <asp:ListItem Value="Outdoor/Carport" Text="Outdoor/Carport"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Panel runat="server" ID="divPreferredSpot" Style="display: none;">
                            <span class="login-text-input-label-desktop" style="width: 300px;">Preferred Garage Spot:<br />
                            </span>
                            <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlPreferredSpot" Width="300">
                                <asp:ListItem Value="Left" Text="Left"></asp:ListItem>
                                <asp:ListItem Value="Right" Text="Right"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Preferred Parking Orientation<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlParkingOrientation" Width="300">
                            <asp:ListItem Value="Front In" Text="Front In"></asp:ListItem>
                            <asp:ListItem Value="Back In" Text="Back In"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">&nbsp;</div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <span style="font-size: 1.2em;">Appliances</span><br />
                        <span style="font-size: 0.9em;">This information is needed to determine available Electrical Load ensuring the charger does not overload your electric system.</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">HVAC Units
                        </span>
                        <br />
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHVACUnits" Width="300">
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Dryer Type
                        </span>
                        <br />
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlDryerType" Width="300">
                            <asp:ListItem Value="Electric" Text="Electric"></asp:ListItem>
                            <asp:ListItem Value="Gas" Text="Gas"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Stove/Range Type
                        </span>
                        <br />
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlStoveType" Width="300">
                            <asp:ListItem Value="Electric" Text="Electric"></asp:ListItem>
                            <asp:ListItem Value="Gas" Text="Gas"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <span style="font-size: 1.2em;">Amenities</span><br />
                        <span style="font-size: 0.9em;">This information is needed to determine available Electrical Load.</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Pool<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHasPool" Width="300">
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Outdoor Hot Tub<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHasHotTub" Width="300">
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Solar Panels<br />
                        </span>
                        <asp:DropDownList runat="server" CssClass="login-text-input-desktop" ID="ddlHasSolarPanels" Width="300">
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Panel runat="server" ID="divSolarSize" Style="display: none;">
                            <span class="login-text-input-label-desktop" style="width: 300px;">Size of Solar Panel System (kW):<br />
                            </span>
                            <asp:TextBox ID="txtSolarSize" runat="server" MaxLength="50" Width="300" CssClass="login-text-input-desktop" placeholder="Size of Solar Panel System"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <span class="login-text-input-label-desktop" style="width: 900px; font-size: 1.0em;">Additional Information or Comments
                        </span>
                        <br />
                        <asp:TextBox ID="txtAdditionalInformation" runat="server" MaxLength="4096" TextMode="MultiLine" Wrap="true" Width="900"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 900px; font-size: 1.2em;">Upload Photos
                        </span>
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Photo of Home From Street<br />
                        </span>
                        <span style="font-size: 0.9em;">Take a photo standing far enough outside your home to capture the entire front of
                                    the home. This helps us to determine the home-to-garage layout.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplHomeFromStreet" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" 
                            MultipleFilesUpload="false" OnFileUploaded="uplHomeFromStreet_FileUploaded" /><br />
                        <asp:TextBox ID="txtHomeFromStreet" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplHomeFromStreet" type="file"/>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgHomeFromStreet" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/1-curb-shot-home-4558-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Photo of Garage<br />
                        </span>
                        <span style="font-size: 0.9em;">Take a photo of your garage from far enough away that we can determine its placement in relation to your home.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplGarage" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)"
                            MultipleFilesUpload="false" ProgressTextID="txtGarage" /><br />
                        <asp:TextBox ID="txtGarage" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplGarage" type="file" />
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgGarage" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/2-garage-45651-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Photo of Garage Interior<br />
                        </span>
                        <span style="font-size: 0.9em;">Stand outside your garage with the doors open and take a photo of the entire garage interior. 
                                    This helps us determine how wiring can be routed.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplGarageInterior" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtGarageInterior" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplGarageInterior" type="file"/>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgGarageInterior" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/3-interior-garage-photo-45611-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Overview Photo of Main Electrical Panel Placement *<br />
                        </span>
                        <span style="font-size: 0.9em;">Take a photo of your main electrical panel <b>standing far enough away</b> that we can see its placement in relation to the rest of your home. 
                                    Your main electrical panel is connected to your electric meter and is most often the larger of your electric panels.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplMainElectricalPanel" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtMainElectricalPanel" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplMainElectricalPanel" type="file"/>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgMainElectricalPanel" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/4-main-electrical-panel-45591-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Close Up Photo of Main Electrical Panel *<br />
                        </span>
                        <span style="font-size: 0.9em;">Open your main electrical panel door and take a photo that includes all the breakers and labels. 
                                    (Please verify that all numbers on breakers can be seen in photo.)<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplMainElectricalPanelCloseUp" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtMainElectricalPanelCloseUp" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplMainElectricalPanelCloseUp" type="file"/>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgMainElectricalPanelCloseUp" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/5-main-electrical-panel-zoom-45601-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Overview Photo of Electrical Sub-Panel Placement<br />
                        </span>
                        <span style="font-size: 0.9em;">Take a photo of your sub-panel <b>standing far enough away</b> that we can see its placement in relation 
                                    to the rest of your garage or home. Your electrical sub-panel is often smaller and located inside your garage or home.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplElectricalSubPanel" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtElectricalSubPanel" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplElectricalSubPanel" type="file"/>
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgElectricalSubPanel" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/6-sub-panel-45621-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Close Up Photo of Electrical Sub-Panel<br />
                        </span>
                        <span style="font-size: 0.9em;">Open your electrical sub-panel door and take a photo that includes all the breakers and labels.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplElectricalSubPanelCloseUp" runat="server" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtElectricalSubPanelCloseUp" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplElectricalSubPanelCloseUp" type="file" />
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgElectricalSubPanelCloseUp" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/7-sub-panel-zoom-45631-225x300.jpg" ImageAlign="Middle" /></td>
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 400px;">Photo of Ideal Charger Location *<br />
                        </span>
                        <span style="font-size: 0.9em;">Take a photo or video of where you think your home charging station should be placed.<br />
                        </span>
                        <%--                        <CuteWebUI:Uploader ID="uplIdealChargerLocation" runat="server" MultipleFilesUpload="false" ValidateOption-AllowedFileExtensions="jpeg,jpg,gif,png" InsertText="Upload (Max 5MB)" /><br />
                        <asp:TextBox ID="txtIdealChargerLocation" runat="server" MaxLength="255" Width="400" CssClass="login-text-input-desktop" ></asp:TextBox>--%>
                        <input id="uplIdealChargerLocation" type="file" />
                    </div>
                    <div class="col-sm">
                        <span class="login-text-input-label-desktop" style="width: 300px;">Example:<br />
                        </span>
                        <asp:Image ID="imgIdealChargerLocation" AlternateText="Example" Height="300" Width="225" runat="server" ImageUrl="~/Content/8-mounting-point-45641-225x300.jpg" ImageAlign="Middle" />
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <input type="button" id="btnSubmitQuoteRequest" class="button-desktop button-default" style="border:none; color:white;" value="Submit" />
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row alert-danger">
                    <asp:Panel CssClass="col-12 alert-danger" ID="divError2" runat="server" Visible="false">
                        <asp:Label runat="server" ID="lblError2" CssClass="alert-danger"></asp:Label>
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnErrorOK" runat="server" CssClass="button-desktop button-default" BorderStyle="None" Text="OK" ForeColor="White"
                                OnClick="btnErrorOK_Click" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <asp:Panel CssClass="col-12 alert-success" ID="divSuccess" runat="server" Visible="false">
                        <asp:Label runat="server" ID="lblSuccess"></asp:Label>
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnSuccessOK" runat="server" CssClass="button-desktop button-default" BorderStyle="None" Text="OK" ForeColor="White"
                                OnClick="btnErrorOK_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="hidSessionID" runat="server" Value="" />
    <script src="https://unpkg.com/dropzone@5/dist/min/dropzone.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/dropzone@5/dist/min/dropzone.min.css" type="text/css" />
    <script src="/Scripts/chargerquote.js" type="module"></script>
</asp:Content>
