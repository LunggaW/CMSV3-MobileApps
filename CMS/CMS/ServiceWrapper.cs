﻿using CMS.DataSource;
using CMS.Models;
using CMS.Properties;
using CMS.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMS
{
    public class ServiceWrapper : IServiceWrapper
    {
        public async Task<TokenModel> GetAuthorizationTokenData(string username, string password)
        {
            TokenModel token = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Username", username),
                    new KeyValuePair<string, string>("Password", password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/token"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();
                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject values = JObject.Parse(response);
                    token = values.ToObject<TokenModel>();
                }
            }
            catch(Exception Ex)
            {
                return null;
            }
            return token;
        }


        public async Task<User> GetUserData(string username, string password, string authenticationToken)
        {
            User userlogged = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("userid", username)
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Users"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();
                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject jobjs = (JObject)JsonConvert.DeserializeObject(response);
                    
                    JToken sitelinks = jobjs["siteProfile"]["profileSiteLinks"];
                    if(sitelinks.Count() > 0)
                    {
                        SQLiteConnection dbConn = DependencyService.Get<ISQLite>().GetConnection();
                        bool commit = false;
                        bool haschange = false;

                        //Profile site
                        //string profsiteid = jobjs["siteProfile"]["siteprofid"].ToString();
                        //string profsitedesc = jobjs["siteProfile"]["siteprofdesc"].ToString();
                        //CMS.Models.ProfileSite profsite = dbConn.Table<CMS.Models.ProfileSite>().FirstOrDefault(d => d.profsiteid.Equals(profsiteid));


                        //if (profsite == null)
                        //{
                        //    profsite = new CMS.Models.ProfileSite();
                        //    profsite.profsiteid = profsiteid;
                        //    profsite.profsitedesc = profsitedesc;
                        //    dbConn.Insert(profsite);
                        //    commit = true;
                        //}
                        //else
                        //{
                        //    if (!profsitedesc.Equals(profsite.profsitedesc))
                        //    {
                        //        profsite.profsitedesc = profsitedesc;
                        //        dbConn.Update(profsite);
                        //        commit = true;
                        //    }
                        //}

                        //if (commit == true)
                        //{
                        //    commit = false;
                        //    dbConn.Commit();
                        //    profsite = null;
                        //}

                        //User
                        string userid = jobjs["userid"].ToString();
                        string uname = jobjs["username"].ToString();
                        string userdesc = jobjs["userdesc"].ToString();
                        int userstatus = Convert.ToInt32(jobjs["userstatus"].ToString());
                        int usertype = Convert.ToInt32(jobjs["usertype"].ToString());
                        string userprofaccid = jobjs["useraccprofile"].ToString();
                        string userprofmenuid = jobjs["usermenuprofile"].ToString();
                        int usersdate = Convert.ToInt32(Convert.ToDateTime(jobjs["userstartdate"].ToString()).ToString("yyyyMMdd"));
                        int useredate = Convert.ToInt32(Convert.ToDateTime(jobjs["userenddate"].ToString()).ToString("yyyyMMdd"));
                        long lastlogin = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                        long lastdown = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                        string imei = jobjs["imei"].ToString();

                        //IMEI
                       //string IMEI = jobjs["IMEI"].ToString();

                        User user = dbConn.Table<User>().FirstOrDefault(d => d.userid.Equals(userid));
                        if (user == null)
                        {
                            user = new User();
                            user.userid = userid;
                            user.username = uname;
                            user.userdesc = userdesc;
                            user.userstatus = userstatus;
                            user.usertype = usertype;
                            user.access_token = authenticationToken;
                            user.logged = 1;
                            user.password = password;
                            user.userprofaccid = userprofaccid;
                            user.userprofmenuid = userprofmenuid;
                            //user.userprofsiteid = profsiteid;
                            user.usersdate = usersdate;
                            user.useredate = useredate;
                            user.lastlogin = lastlogin;
                            user.lastdown = lastdown;
                            user.imei = imei;
                            dbConn.Insert(user);
                            commit = true;
                        }
                        else
                        {
                            if (!uname.Equals(user.username))
                            {
                                user.username = uname;
                                haschange = true;
                            }
                            if (!userdesc.Equals(user.userdesc))
                            {
                                user.userdesc = userdesc;
                                haschange = true;
                            }
                            if (userstatus != user.userstatus)
                            {
                                user.userstatus = userstatus;
                                haschange = true;
                            }
                            if (usertype != user.usertype)
                            {
                                user.usertype = usertype;
                                haschange = true;
                            }
                            if (!userprofaccid.Equals(user.userprofaccid))
                            {
                                user.userprofaccid = userprofaccid;
                                haschange = true;
                            }
                            if (!userprofmenuid.Equals(user.userprofmenuid))
                            {
                                user.userprofmenuid = userprofmenuid;
                                haschange = true;
                            }
                            //if (!profsiteid.Equals(user.userprofsiteid))
                            //{
                            //    user.userprofsiteid = profsiteid;
                            //    haschange = true;
                            //}
                            if (usersdate != user.usersdate)
                            {
                                user.usersdate = usersdate;
                                haschange = true;
                            }
                            if (useredate != user.useredate)
                            {
                                user.useredate = useredate;
                                haschange = true;
                            }
                            if (haschange == true)
                            {
                                user.access_token = authenticationToken;
                                user.logged = 1;
                                user.password = password;
                                user.lastlogin = lastlogin;
                                user.lastdown = lastdown;
                                dbConn.Update(user);
                                commit = true;
                            }
                        }

                        if (commit == true)
                        {
                            haschange = false;
                            commit = false;
                            dbConn.Commit();
                        }

                        foreach (JObject sitelink in sitelinks)
                        {
                            haschange = false;
                            //Site
                            string siteid = sitelink["site"]["siteid"].ToString();
                            string sitename = sitelink["site"]["sitename"].ToString();
                            int siteclass = Convert.ToInt32(sitelink["site"]["siteclass"].ToString());
                            int sitestatus = Convert.ToInt32(sitelink["site"]["sitestatus"].ToString());
                            int siteflag = Convert.ToInt32(sitelink["site"]["siteflag"].ToString());

                            Site newsite = dbConn.Table<Site>().FirstOrDefault(d => d.siteid.Equals(siteid));
                            if (newsite == null)
                            {
                                newsite = new Site();
                                newsite.siteid = siteid;
                                newsite.sitename = sitename;
                                newsite.siteclass = siteclass;
                                newsite.sitestatus = sitestatus;
                                newsite.siteflag = siteflag;
                                dbConn.Insert(newsite);
                                commit = true;
                            }
                            else
                            {
                                if (!sitename.Equals(newsite.sitename))
                                {
                                    newsite.sitename = sitename;
                                    haschange = true;
                                }
                                if (siteclass != newsite.siteclass)
                                {
                                    newsite.siteclass = siteclass;
                                    haschange = true;
                                }
                                if (sitestatus != newsite.sitestatus)
                                {
                                    newsite.sitestatus = sitestatus;
                                    haschange = true;
                                }
                                if (siteflag != newsite.siteflag)
                                {
                                    newsite.siteflag = siteflag;
                                    haschange = true;
                                }
                                if (haschange == true)
                                {
                                    dbConn.Update(newsite);
                                    commit = true;
                                }
                            }

                            if (commit == true)
                            {
                                haschange = false;
                                commit = false;
                                dbConn.Commit();
                                newsite = null;
                            }

                            //Profile site link
                            int proflinksdate = Convert.ToInt32(Convert.ToDateTime(sitelink["linksdate"].ToString()).ToString("yyyyMMdd"));
                            int proflinkedate = Convert.ToInt32(Convert.ToDateTime(sitelink["linkedate"].ToString()).ToString("yyyyMMdd"));

                            ProfileSiteLink profsitelink = dbConn.Table<ProfileSiteLink>().FirstOrDefault(d =>
                                d.siteid.Equals(siteid) &&
                                d.userid.Equals(user.userid) &&
                                d.linksdate == proflinksdate
                            );

                            if (profsitelink == null)
                            {
                                profsitelink = new ProfileSiteLink();
                                profsitelink.siteid = siteid;
                                profsitelink.userid = user.userid;
                                profsitelink.linksdate = proflinksdate;
                                profsitelink.linkedate = proflinkedate;
                                dbConn.Insert(profsitelink);
                                commit = true;
                            }
                            else
                            {
                                if (proflinkedate != profsitelink.linkedate)
                                {
                                    dbConn.Execute("update [profsitelink] set linkedate = " + proflinkedate + " where userid = '" + user.userid + "' and siteid = '" + siteid + "' and linksdate = " + proflinksdate);
                                    commit = true;
                                }
                            }

                            if (commit == true)
                            {
                                haschange = false;
                                commit = false;
                                dbConn.Commit();
                                profsitelink = null;
                            }

                            JToken skulinks = sitelink["site"]["skuLink"];
                            if (skulinks.Count() > 0)
                            {
                                foreach (JObject skulink in skulinks)
                                {
                                    haschange = false;
                                    //Brand
                                    string brandid = skulink["jBrand"]["brandid"].ToString();
                                    string branddesc = skulink["jBrand"]["branddesc"].ToString();
                                    Brand brand = dbConn.Table<Brand>().FirstOrDefault(d => d.brandid.Equals(brandid));
                                    if (brand == null)
                                    {
                                        brand = new Brand();
                                        brand.brandid = brandid;
                                        brand.branddesc = branddesc;
                                        dbConn.Insert(brand);
                                        commit = true;
                                    }
                                    else
                                    {
                                        if (!branddesc.Equals(brand.branddesc))
                                        {
                                            brand.branddesc = branddesc;
                                            dbConn.Update(brand);
                                            commit = true;
                                        }
                                    }

                                    if (commit == true)
                                    {
                                        haschange = false;
                                        commit = false;
                                        dbConn.Commit();
                                        brand = null;
                                    }

                                    //Sku header
                                    int skuid = Convert.ToInt32(skulink["skuHeader"]["skuid"].ToString());
                                    string skuidx = skulink["skuHeader"]["skuidx"].ToString();
                                    string skusdesc = skulink["skuHeader"]["skusdesc"].ToString();
                                    string skuldesc = skulink["skuHeader"]["skuldesc"].ToString();
                                    int skusdate = Convert.ToInt32(Convert.ToDateTime(skulink["skuHeader"]["skusdate"].ToString()).ToString("yyyyMMdd"));
                                    int skuedate = Convert.ToInt32(Convert.ToDateTime(skulink["skuHeader"]["skuedate"].ToString()).ToString("yyyyMMdd"));

                                    SkuHeader skuh = dbConn.Table<SkuHeader>().FirstOrDefault(d => d.skuid == skuid);
                                    if (skuh == null)
                                    {
                                        skuh = new SkuHeader();
                                        skuh.skuid = skuid;
                                        skuh.skuidx = skuidx;
                                        skuh.skusdesc = skusdesc;
                                        skuh.skuldesc = skuldesc;
                                        skuh.skusdate = skusdate;
                                        skuh.skuedate = skuedate;
                                        dbConn.Insert(skuh);
                                        commit = true;
                                    }
                                    else
                                    {
                                        if (!skuidx.Equals(skuh.skuidx))
                                        {
                                            skuh.skuidx = skuidx;
                                            haschange = true;
                                        }
                                        if (!skusdesc.Equals(skuh.skusdesc))
                                        {
                                            skuh.skusdesc = skusdesc;
                                            haschange = true;
                                        }
                                        if (!skuldesc.Equals(skuh.skuldesc))
                                        {
                                            skuh.skuldesc = skuldesc;
                                            haschange = true;
                                        }
                                        if (skusdate != skuh.skusdate)
                                        {
                                            skuh.skusdate = skusdate;
                                            haschange = true;
                                        }
                                        if(skuedate != skuh.skuedate)
                                        {
                                            skuh.skuedate = skuedate;
                                            haschange = true;
                                        }
                                        if(haschange == true)
                                        {
                                            dbConn.Update(skuh);
                                            commit = true;
                                        }
                                    }

                                    if (commit == true)
                                    {
                                        haschange = false;
                                        commit = false;
                                        dbConn.Commit();
                                        skuh = null;
                                    }

                                    //Sku Link
                                    int skulinksdate = Convert.ToInt32(Convert.ToDateTime(skulink["linksdate"].ToString()).ToString("yyyyMMdd"));
                                    int skulinkedate = Convert.ToInt32(Convert.ToDateTime(skulink["linkedate"].ToString()).ToString("yyyyMMdd"));
                                    SkuLink newskulink = dbConn.Table<SkuLink>().FirstOrDefault(d =>
                                        d.siteid.Equals(siteid) &&
                                        d.brandid.Equals(brandid) &&
                                        d.skuid == skuid &&
                                        d.linksdate == skulinksdate
                                    );
                                    if (newskulink == null)
                                    {
                                        newskulink = new SkuLink();
                                        newskulink.siteid = siteid;
                                        newskulink.skuid = skuid;
                                        newskulink.brandid = brandid;
                                        newskulink.linksdate = skulinksdate;
                                        newskulink.linkedate = skulinkedate;
                                        dbConn.Insert(newskulink);
                                        commit = true;
                                    }
                                    else
                                    {
                                        if(skulinkedate != newskulink.linkedate)
                                        {
                                            dbConn.Update("update [skulink] set linkedate = " + skulinkedate + " where siteid = '" + siteid + "' and brandid = '" + brandid + "' and skuid = " + skuid + " and linksdate = " + skulinksdate);
                                            commit = true;
                                        }
                                    }

                                    if (commit == true)
                                    {
                                        haschange = false;
                                        commit = false;
                                        dbConn.Commit();
                                        newskulink = null;
                                    }
                                }
                            }
                        }
                        userlogged = user;
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return userlogged;
        }

        public async Task<bool> UploadSales(User user, List<Transaction> trans)
        {
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {
                    List<JSalesModel> jsales = new List<JSalesModel>();
                    foreach (Transaction ts in trans)
                    {
                        JSalesModel js = new JSalesModel();
                        js.transid = ts.transid;
                        js.transnota = ts.transnota;
                        js.transsite = ts.transsite;
                        js.transdate = Convert.ToDateTime(ts.transdate.ToString().Substring(0, 4) + "-" + ts.transdate.ToString().Substring(4, 2) + "-" + ts.transdate.ToString().Substring(6, 2));
                        js.transbrcd = ts.transbrcd;
                        js.transsku = ts.transsku;
                        js.transqty = ts.transqty;
                        js.transprice = ts.transprice;
                        js.transamt = ts.transamt;
                        js.transstat = ts.transstat;
                        js.transtype = ts.transtype;
                        js.transflag = ts.transflag;
                        js.transdcre = Convert.ToDateTime(ts.transdcre.ToString().Substring(0, 4) + "-" + ts.transdcre.ToString().Substring(4, 2) + "-" + ts.transdcre.ToString().Substring(6, 2) + "T" + ts.transdcre.ToString().Substring(8, 2) + ":" + ts.transdcre.ToString().Substring(10, 2) + ":" + ts.transdcre.ToString().Substring(12, 2));
                        js.transcreby = ts.transcreby;

                        //Update GAGAN
                        js.transdiscount = ts.transdiscount;
                        js.transfinalprice = ts.transfinalprice;

                        jsales.Add(js);
                    }

                    string sales = JsonConvert.SerializeObject(jsales);
                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + user.access_token);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("userid", user.userid),
                        new KeyValuePair<string, string>("salesdata", sales)
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();

                        DSTransaction dstrans = new DSTransaction();

                        foreach(Transaction ts in trans)
                        {
                            dstrans.Delete(ts);
                        }

                        retval = true;
                    }
                }
                catch
                {
                    retval = false;
                }
                
            }
            return retval;
        }

        public async Task<bool> UploadComplexSales(User user, List<Transaction> trans)
        {
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {
                    List<JSalesModel> jsales = new List<JSalesModel>();
                    foreach (Transaction ts in trans)
                    {
                        JSalesModel js = new JSalesModel();
                        js.transnota = ts.transnota;
                        js.transsite = ts.transsite;
                        js.transdate = Convert.ToDateTime(ts.transdate.ToString().Substring(0, 4) + "-" + ts.transdate.ToString().Substring(4, 2) + "-" + ts.transdate.ToString().Substring(6, 2));
                        js.transbrcd = ts.transbrcd;
                        js.transsku = ts.transsku;
                        js.transqty = ts.transqty;
                        js.transamt = ts.transamt;
                        js.transstat = ts.transstat;
                        js.transflag = ts.transflag;
                        js.transdcre = Convert.ToDateTime(ts.transdcre.ToString().Substring(0, 4) + "-" + ts.transdcre.ToString().Substring(4, 2) + "-" + ts.transdcre.ToString().Substring(6, 2) + "T" + ts.transdcre.ToString().Substring(8, 2) + ":" + ts.transdcre.ToString().Substring(10, 2) + ":" + ts.transdcre.ToString().Substring(12, 2));
                        js.transcreby = ts.transcreby;
                        js.transcomm = ts.transcomm;
                        js.transiscomplex = ts.transiscomplex;
                        

                        jsales.Add(js);
                    }

                    string sales = JsonConvert.SerializeObject(jsales);
                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + user.access_token);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("userid", user.userid),
                        new KeyValuePair<string, string>("salesdata", sales)
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales/Complex"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();

                        DSTransaction dstrans = new DSTransaction();

                        foreach (Transaction ts in trans)
                        {
                            dstrans.Delete(ts);
                        }

                        retval = true;
                    }
                }
                catch
                {
                    retval = false;
                }

            }
            return retval;
        }

        public async Task<bool> ChangePassword(User user)
        {
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {
                    
                    
                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + user.access_token);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("userid", user.userid),
                        new KeyValuePair<string, string>("password", user.password)
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Users/changepassword"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();

                        


                        retval = true;
                    }
                }
                catch
                {
                    retval = false;
                }

            }
            return retval;
        }

        public async Task<string> GetImei(String UserID, String AuthenticationToken)
        {
            string response = "";
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AuthenticationToken);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("userid", UserID),
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Users/getimei"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();
                        response = await postResponse.Content.ReadAsStringAsync();
                    }
                }
                catch
                {
                    return "";
                }

            }
            return response;
        }

        public async Task<string> GetPrice(String Barcode, String Site, String UserID,  String AuthenticationToken)
        {
            string response = "";
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {


                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AuthenticationToken);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("barcode", Barcode),
                        new KeyValuePair<string, string>("site", Site),
                        new KeyValuePair<string, string>("userid", UserID),
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Price/getprice"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();
                        response = await postResponse.Content.ReadAsStringAsync();

                        response = response.Replace("\"", "");
                        return response;
                    }
                }
                catch
                {
                    return "";
                }

            }
            return response;
        }

        public async Task<bool> CheckNotaAndBarcode(JSalesHeaderPrimary jSalesHeaderPrimary, String AuthenticationToken)
        {
            string response = "";
            bool retval = false;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                try
                {
                    string salesHeader = JsonConvert.SerializeObject(jSalesHeaderPrimary);

                    HttpClientHandler handler = new HttpClientHandler();
                    var client = new HttpClient(handler);
                    client.MaxResponseContentBufferSize = 256000;
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AuthenticationToken);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("salesheader", salesHeader)
                    });

                    var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales/checknotabarcode"), formContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        postResponse.EnsureSuccessStatusCode();
                        //response = await postResponse.Content.ReadAsStringAsync();

                        //response = response.Replace("\"", "");
                        return true;
                        
                    }
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<IEnumerable<SkuList>> GetSkuLists(String Barcode, String Site, String UserID, String authenticationToken)
        {
            User userlogged = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("barcode", Barcode),
                    new KeyValuePair<string, string>("site", Site),
                    new KeyValuePair<string, string>("userid", UserID)
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sku/getskulists"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();

                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject jobjs = (JObject)JsonConvert.DeserializeObject(response);
                    
                    
                    JToken skuLists = jobjs["skuList"];

                    if (skuLists.Any())
                    {

                        List<SkuList> skuListsList = new List<SkuList>();

                        SkuList skuListObject = new SkuList();

                        foreach (JObject skulist in skuLists)
                        {
                            skuListObject = new SkuList();

                            skuListObject.id = Int32.Parse(skulist["skuid"].ToString());
                            skuListObject.name = skulist["skuDesc"].ToString();

                            skuListsList.Add(skuListObject);
                            //JToken sitelinks = jobjs["siteProfile"]["profileSiteLinks"];
                            
                        }

                        return skuListsList;


                    }
                    else
                    {
                        return null;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<JSalesHeader>> GetSalesHeader(JSalesHeaderPrimary salesHeaderPrimary, String authenticationToken)
        {
            User userlogged = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

                string salesHeader = JsonConvert.SerializeObject(salesHeaderPrimary);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("salesheader", salesHeader)
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales/getsalesheader"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();

                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject jobjs = (JObject)JsonConvert.DeserializeObject(response);


                    JToken salesHeaderLists = jobjs["salesHeaderLists"];

                    if (salesHeaderLists.Any())
                    {

                        List<JSalesHeader> salesHeaderListsList = new List<JSalesHeader>();

                        JSalesHeader salesHeaderObj = new JSalesHeader();

                        foreach (JObject salesHeaderTemp in salesHeaderLists)
                        {
                            salesHeaderObj = new JSalesHeader();

                            salesHeaderObj.nota = salesHeaderTemp["nota"].ToString();
                            salesHeaderObj.site = salesHeaderTemp["site"].ToString();
                            salesHeaderObj.user = salesHeaderTemp["user"].ToString();
                            salesHeaderObj.date = DateTime.Parse(salesHeaderTemp["date"].ToString());
                            salesHeaderObj.totalamount = decimal.Parse(salesHeaderTemp["totalamount"].ToString());
                            salesHeaderObj.SalesType = (JSalesHeader.SalesTypeEnum)Int32.Parse(salesHeaderTemp["salesType"].ToString());
                            salesHeaderObj.SalesStatus = (JSalesHeader.SalesStatusEnum)Int32.Parse(salesHeaderTemp["salesStatus"].ToString());

                            salesHeaderListsList.Add(salesHeaderObj);
                            

                        }

                        return salesHeaderListsList;


                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<JSalesHeader>> GetSalesHeaderByStatusTypeSales(JSalesHeaderPrimary salesHeaderPrimary, String authenticationToken)
        {
            User userlogged = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

                string salesHeader = JsonConvert.SerializeObject(salesHeaderPrimary);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("salesheader", salesHeader)
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales/getsalesheaderbystatustype"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();

                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject jobjs = (JObject)JsonConvert.DeserializeObject(response);


                    JToken salesHeaderLists = jobjs["salesHeaderLists"];

                    if (salesHeaderLists.Any())
                    {

                        List<JSalesHeader> salesHeaderListsList = new List<JSalesHeader>();

                        JSalesHeader salesHeaderObj = new JSalesHeader();

                        foreach (JObject salesHeaderTemp in salesHeaderLists)
                        {
                            salesHeaderObj = new JSalesHeader();

                            salesHeaderObj.nota = salesHeaderTemp["nota"].ToString();
                            salesHeaderObj.site = salesHeaderTemp["site"].ToString();
                            salesHeaderObj.user = salesHeaderTemp["user"].ToString();
                            salesHeaderObj.date = DateTime.Parse(salesHeaderTemp["date"].ToString());
                            salesHeaderObj.totalamount = decimal.Parse(salesHeaderTemp["totalamount"].ToString());
                            salesHeaderObj.SalesType = (JSalesHeader.SalesTypeEnum)Int32.Parse(salesHeaderTemp["salesType"].ToString());
                            salesHeaderObj.SalesStatus = (JSalesHeader.SalesStatusEnum)Int32.Parse(salesHeaderTemp["salesStatus"].ToString());
                            salesHeaderObj.qty = Int32.Parse(salesHeaderTemp["qty"].ToString());

                            salesHeaderListsList.Add(salesHeaderObj);


                        }

                        return salesHeaderListsList;


                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<JSalesDetail>> GetSalesDetail(JSalesHeaderPrimary salesHeaderPrimary, String authenticationToken)
        {
            User userlogged = null;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                var client = new HttpClient(handler);
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

                string salesHeader = JsonConvert.SerializeObject(salesHeaderPrimary);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("salesdet", salesHeader)
                });

                var postResponse = await client.PostAsync(new Uri(Resources.APIURL + "/api/Sales/getsalesdet"), formContent);
                if (postResponse.IsSuccessStatusCode)
                {
                    postResponse.EnsureSuccessStatusCode();

                    string response = await postResponse.Content.ReadAsStringAsync();
                    JObject jobjs = (JObject)JsonConvert.DeserializeObject(response);


                    JToken salesDetailLists = jobjs["salesDetailLists"];

                    if (salesDetailLists.Any())
                    {

                        List<JSalesDetail> salesDetListsList = new List<JSalesDetail>();

                        JSalesDetail salesDetObj = new JSalesDetail();

                        foreach (JObject salesDetailTemp in salesDetailLists)
                        {
                            salesDetObj = new JSalesDetail();

                            salesDetObj.nota = salesDetailTemp["nota"].ToString();
                            salesDetObj.site = salesDetailTemp["site"].ToString();
                            salesDetObj.userApps = salesDetailTemp["userApps"].ToString();
                            salesDetObj.salesdate = DateTime.Parse(salesDetailTemp["salesdate"].ToString());
                            salesDetObj.barcode = salesDetailTemp["barcode"].ToString();
                            salesDetObj.qty = int.Parse(salesDetailTemp["qty"].ToString());
                            salesDetObj.totalamount = decimal.Parse(salesDetailTemp["totalamount"].ToString());
                            salesDetObj.itemid = salesDetailTemp["itemid"].ToString();
                            salesDetObj.variant = salesDetailTemp["variant"].ToString();
                            salesDetObj.description = salesDetailTemp["description"].ToString();
                            salesDetObj.sku = salesDetailTemp["sku"].ToString();
                            salesDetObj.gross = decimal.Parse(salesDetailTemp["gross"].ToString());
                            salesDetObj.SalesType = (JSalesDetail.SalesTypeEnum)Enum.Parse(typeof(JSalesDetail.SalesTypeEnum), salesDetailTemp["salesType"].ToString());
                            salesDetObj.SalesStatus = (JSalesDetail.SalesStatusEnum)Enum.Parse(typeof(JSalesDetail.SalesStatusEnum), salesDetailTemp["salesStatus"].ToString());

                            salesDetListsList.Add(salesDetObj);


                        }

                        return salesDetListsList;


                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
