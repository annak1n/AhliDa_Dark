using UnityEngine;
using System.Collections;
using KBEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UI_LoginSystem : MonoBehaviour {
    public static UI inst;
    public int ui_state = 0;
    private string stringAccount = "";
    private string stringPasswd = "";
   //* private string labelMsg = "";
    private Color labelColor = Color.green;


    private Dictionary<UInt64, AVATAR_INFOS> ui_avatarList = null;

    private string stringAvatarName = "";
    private bool startCreateAvatar = false;

    private UInt64 selAvatarDBID = 0;
    public bool showReliveGUI = false;

    bool startRelogin = false;
    public InputField if_userName;
    public InputField if_passWord;
    public Text text_status;
    public Text version_info_client;
    public Text version_info_server;

    

    // Use this for initialization
    void Start()
    {
        // common
        KBEngine.Event.registerOut("onKicked", this, "onKicked");
        KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
        KBEngine.Event.registerOut("onConnectionState", this, "onConnectionState");
        KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");

        // login
        KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
        KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
        KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
        KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
        KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
        KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        KBEngine.Event.registerOut("onReloginBaseappFailed", this, "onReloginBaseappFailed");
        KBEngine.Event.registerOut("onReloginBaseappSuccessfully", this, "onReloginBaseappSuccessfully");
        KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
        KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
        KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
        KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");

        // select-avatars(register by scripts)
     //*   KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
     //*   KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
     //*   KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
    }

    void OnDestroy()
    {
        KBEngine.Event.deregisterOut(this);
    }

    public void onConnectStatus(bool beSuccess)
    {
        if (beSuccess)
        {
            print("Successful connection");
            text_status.text = "Successful connection";
        }
        else
        {
            text_status.text = "Connection error";
        }
    }

    public void onLoginFailed(UInt16 errorCode)
    {
        text_status.text = "Login failed: " + KBEngineApp.app.serverErr(errorCode);
    }

    public void onLoginSuccessfully(UInt64 uuuid, Int32 id, Account account)
    {
        if (account != null)
        {
            text_status.text = "Login successful!";
            ui_state = 1;
            SceneManager.LoadScene("selavatars");
        }
    }

    public void onLogin()
    {
        print("connect to server...(Connect to server...)");
        text_status.text = "Connect to server...";
        KBEngine.Event.fireIn("login", if_userName.text, if_passWord.text, System.Text.Encoding.UTF8.GetBytes("lxqLogin"));
    }
    public void onRegister()
    {
        text_status.text = "Connect to server...";
        KBEngine.Event.fireIn("createAccount", if_userName.text, if_passWord.text, System.Text.Encoding.UTF8.GetBytes("lxqRegister"));
    }
    public void onCreateAccountResult(UInt16 retcode, byte[] datas)
    {
        if (retcode != 0)
        {
            print("(Wrong registration account)! err=" + KBEngineApp.app.serverErr(retcode));
            text_status.text = "(Wrong registration account)! err=" + KBEngineApp.app.serverErr(retcode);
            return;
        }
        text_status.text = "Registered account is successful!";
    }

    // Update is called once per frame
    void Update()
    {
     //*   version_info_client.text = "Client Version: " + KBEngine.KBEngineApp.app.clientVersion;
     //*   version_info_server.text = "Server Version: " + KBEngine.KBEngineApp.app.serverVersion;
    }

   public void onKicked(UInt16 failedcode)
    {

        MNPopup popup = new MNPopup("U has been kicked", "reason:" + KBEngineApp.app.serverErr(failedcode));
        popup.AddAction("Ok", () => {
            SceneManager.LoadScene("login");
            ui_state = 0;
        });
        popup.Show();
    }

    public void onDisconnected()
    {
            text_status.text="disconnect! will try to reconnect...";
            startRelogin = true;
            Invoke("onReloginBaseappTimer", 1.0f);
    }

    public void onConnectionState(bool success)
    {
        if (!success)
            text_status.text="connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! ";
        else
            text_status.text = "connect successfully, please wait...";
    }

    public void onVersionNotMatch(string verInfo, string serVerInfo)
    {
        text_status.text = "";
    }

    public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
    {
        text_status.text = "";
    }

    public void onLoginBaseappFailed(UInt16 failedcode)
    {
        text_status.text = "loginBaseapp is failed, err=" + KBEngineApp.app.serverErr(failedcode);
    }

    public void onLoginBaseapp()
    {
        text_status.text = "connect to loginBaseapp, please wait...";
    }

    public void onReloginBaseappFailed(UInt16 failedcode)
    {
        text_status.text = "relogin is failed, err=" + KBEngineApp.app.serverErr(failedcode);
        startRelogin = false;
    }

    public void onReloginBaseappSuccessfully()
    {
        text_status.text = "relogin is successfully!";
        startRelogin = false;
    }

    public void Loginapp_importClientMessages()
    {
        text_status.text = "Loginapp_importClientMessages ...";
    }

    public void Baseapp_importClientMessages()
    {
        text_status.text = "Baseapp_importClientMessages ...";
    }

    public void Baseapp_importClientEntityDef()
    {
        text_status.text = "importClientEntityDef ...";
    }

    public void onClose()
    {
        Application.Quit();
    }
}
