using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 界面管理类
/// </summary>
public class UIManager {

    private static UIManager _instance;

    public static UIManager Instance {
        get {
            if (_instance == null) {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 界面根节点
    /// </summary>
    private Transform _root;

    public Transform UIRoot {
        get {
            if (_root == null) {
                _root = GameObject.Find("UI Root/Camera").transform;
            }
            return _root;
        }
    }
    //private Transform _root_scene;
    //public Transform CacheRootScene
    //{
    //    get
    //    {
    //        if (_root_scene == null)
    //        {
    //            _root_scene = GameObject.Find("UI Root/UI/CameraScene").transform;
    //        }
    //        return _root_scene;
    //    }
    //}

    /// <summary>
    /// 显示某个界面
    /// </summary>
    /// <param name="uiname"></param>
    /// <returns></returns>
    public KPresenter Show(string uiname) {
        GameObject ui = FindUI(uiname);
        KPresenter page = Show(ui);
        return page;
    }

    /// <summary>
    /// 获得UI类
    /// </summary>
    /// <param name="uiname"></param>
    /// <returns></returns>
    public KPresenter GetPresenter(string uiname) {
        GameObject ui = FindUI(uiname);
        if (ui != null)
            return ui.GetComponent<KPresenter>();
        return null;
    }

    private KPresenter Show(GameObject ui) {
        KPresenter page = ui.GetComponent<KPresenter>();
        if (page != null) {
            Show(page);
        }
        else {
            ui.SetActive(true);
        }
        return page;
    }

    public void Show(KPresenter page) {
        page.OnShowing();
        page.gameObject.SetActive(true);
        page.OnShown();
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="uiname"></param>
    public void Hide(string uiname) {
        GameObject ui = FindUI(uiname);
        Hide(ui);
    }

    public void Hide(GameObject ui) {
        KPresenter page = ui.GetComponent<KPresenter>();
        if (page != null) {
            Hide(page);
        }
        else {
            ui.SetActive(false);
        }
    }

    public void Hide(KPresenter page) {
        page.OnClosing();
        page.gameObject.SetActive(false);
        page.OnClosed();
    }

    public void HideAll() {
        for (int i = 0; i < UIRoot.childCount; i++)
        {
            UIRoot.GetChild(i).gameObject.SetActive(false);
        }
    }

    private GameObject FindUI(string name) {

        Transform ui = UIRoot.FindChild(name);

        if (ui == null) {
            throw new Exception("无法在" + _root.name + "下找到界面：" + name);
        }
        return ui.gameObject;
    }
}