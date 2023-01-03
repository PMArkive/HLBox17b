using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using HLBox17b.Classes.Tools;
using System.Drawing;

public struct stRegConf
{
	public string szGen_17bUrl;
	public string szGen_17bUpdateUrl;
	public string szGen_17bMInfosUrl;
	public string szGen_17bTexesHelp;
	public string szGen_Language;
	public string szGen_SteamPath;
	public string szGen_SteamLibrariesPath;
	public string szGen_DLTarget;
	public string szGen_LastPath;
	public bool bGens_NoSteam;
	public string szGen_GcfsPath;
	public bool bGen_Simulate;
	public bool bGen_ShowSplash;
	public bool bGen_AskBeforeExit;
	public bool bGen_CheckVerAtStartup;
	public int nGens_HistorySize;
	public int nPack_OneForAll;
	public int nPack_EmptyPath;
	public bool bPack_CreateRes;
	public bool bPack_OverwriteRes;
	public bool bPack_IncludeWads;
	public bool bPack_PakReplace;
	public int nPack_HistorySize;
	public string szPack_LastPath;
	public int nPack_ZipLevel;
	public string szPack_TargetPath;
	public bool bInst_AnalysePath;
	public string szInst_MiscPath;
	public bool bInst_Replace;
	public bool bInst_AskWhenDiffers;
	public bool bInst_AutoDetectFolder;
	public int nInst_HistorySize;
	public string szInst_TargetPath;
	public string szInst_LastPath;
	public string szTxs_LastPath;
	public string szTxs_LastExportPath;
	public string szTxs_LastImportPath;
	public int nTxs_LastNewWidth;
	public int nTxs_LastNewHeight;
	public string szRes_LastPath;
	public string szXpl_LastExtractPath;
	public string szXpl_LastInputPath;
	public bool bPackage;
	public bool bOneForAll;
	public string szQry_TargetPath;
	public int nQry_FileType;
	public string szBan_defaultUrl;
	public bool bRes_CommentUnnecessaryFiles;
	public int nQry_HistorySize;
	public int nTxs_DefThumbSize;
	public string szTxs_DefImgType;
	public string szTxs_DefEditor;
	public int nTxs_NewColor1;
	public int nTxs_NewColor2;
	public string szVtf_LastBrowserPath;
	public bool bSys_AssociateBsp;
	public bool bSys_AssociatePck;
	public bool bSys_AssociateTex;
	public bool bSys_CheckAndRestore;
	public int nDwn_DefAppId;
	public int nCln_LastMod;
	public string szHlBoxTmpPath;
	}

namespace HLBox17b
	{
	class myConf
		{
		/// <summary>
		/// Default Construtor set reg connection string
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public myConf()
			{
			}

		/// <summary>
		/// Recuperer toutes les valeurs de configuration
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public stRegConf GetMainConfig()
			{
			stRegConf rc = new stRegConf();

			RegistryKey rk;
			rk = RegTools.OpenOrCreateRegKey("", false);
			//General Conf
			rc.bPackage = false;
			rc.bOneForAll = true;
			rc.szGen_17bUrl = (string)rk.GetValue("sz17bUrl", "http://www.17buddies.net");
			rc.szGen_17bUpdateUrl = (string)rk.GetValue("sz17bUpdateUrl", "http://www.17buddies.net/17b2/Tools/17b2-HLBox.php?CheckVer=1");
			rc.szGen_17bMInfosUrl = (string)rk.GetValue("sz17bMInfosUrl", "http://www.17buddies.net/17b2/Tools/17b2-HLBox.php");
			rc.szGen_17bTexesHelp = (string)rk.GetValue("sz17bTexesHelp", "http://forum.17buddies.net/index.php?showtopic=13727");
			rc.szGen_Language = (string)rk.GetValue("szLanguage", "en");
			rc.szGen_DLTarget = (string)rk.GetValue("szDLTarget", "");
			rc.szGen_LastPath = (string)rk.GetValue("szGenLastPath", "");
			rc.bGen_ShowSplash = ((int)rk.GetValue("bShowSplash", 1) == 1) ? true : false;
			rc.bGen_AskBeforeExit = ((int)rk.GetValue("bAskBeforeExit", 1) == 1) ? true : false;
			rc.bGen_CheckVerAtStartup = ((int)rk.GetValue("bCheckVerAtStartup", 0) == 1) ? true : false;
			rc.bGen_Simulate = ((int)rk.GetValue("bSimulate", 0) == 1) ? true : false;
			rc.nGens_HistorySize = (int)rk.GetValue("nGensHistorySize", 16);
			rc.szGen_GcfsPath = "";
			rc.szGen_SteamPath = MiscTools.GetSteamInstallPath();
			//////////////////////////////////////////////////
			rc.szGen_SteamLibrariesPath = rc.szGen_SteamPath;
			//////////////////////////////////////////////////
			rc.bGens_NoSteam = false;
			if (rc.szGen_SteamPath != "")
				rc.szGen_GcfsPath = rc.szGen_SteamPath + "\\steamapps\\";
			else
				rc.bGens_NoSteam = true;
			//////////////////////////////////////////
			//rc.bGens_NoSteam =true;
			//////////////////////////////////////////
			//Ims Conf
			rk = RegTools.OpenOrCreateRegKey("Pack", true);
			rc.nPack_OneForAll = (int)rk.GetValue("nPackOneForAll", 0);
			rc.nPack_EmptyPath = (int)rk.GetValue("nPackEmptyPath", 0);
			rc.bPack_CreateRes = ((int)rk.GetValue("bPackCreateRes", 1) == 1) ? true : false;
			rc.bPack_PakReplace = ((int)rk.GetValue("bPackPakReplace", 1) == 1) ? true : false;
			rc.bPack_OverwriteRes = ((int)rk.GetValue("bPackOverwriteRes", 0) == 1) ? true : false;
			rc.bPack_IncludeWads = ((int)rk.GetValue("bPackIncludeWads", 1) == 1) ? true : false;
			rc.szPack_TargetPath = (string)rk.GetValue("szPackTargetPath", "");
			rc.szPack_LastPath = (string)rk.GetValue("szPackLastPath", "");
			rc.nPack_HistorySize = (int)rk.GetValue("nPackHistorySize", 16);
			rc.nPack_ZipLevel = (int)rk.GetValue("nPackZipLevel", 1);

			//Smi Conf
			rk = RegTools.OpenOrCreateRegKey("Inst", true);
			rc.szInst_MiscPath = (string)rk.GetValue("szInstMiscPath", "misc");
			rc.bInst_Replace = ((int)rk.GetValue("bInstReplace", 0) == 1) ? true : false;
			rc.bInst_AskWhenDiffers = ((int)rk.GetValue("bInstAskWhenDiffers", 0) == 1) ? true : false;
			rc.bInst_AutoDetectFolder = ((int)rk.GetValue("bInstAutoDetectFolder", 0) == 1) ? true : false;
			rc.bInst_AnalysePath = ((int)rk.GetValue("bInstAnalysePath", 1) == 1) ? true : false;
			rc.szInst_LastPath = (string)rk.GetValue("szInstLastPath", "");
			rc.szInst_TargetPath = (string)rk.GetValue("szInstTargetPath", "");
			rc.nInst_HistorySize = (int)rk.GetValue("nInstHistorySize", 16);

			//Qry Conf
			rk = RegTools.OpenOrCreateRegKey("Qry", true);
			rc.szQry_TargetPath = (string)rk.GetValue("szQryTargetPath", "");
			rc.nQry_FileType = (int)rk.GetValue("nQryFileType", 1);
			rc.nQry_HistorySize = (int)rk.GetValue("nQryHistorySize", 16);

			//Ban Conf
			rk = RegTools.OpenOrCreateRegKey("Ban", true);
			rc.szBan_defaultUrl = (string)rk.GetValue("szBanDefaultUrl", "http://www.17buddies.net/17b2/Images/Bangfx.html");

			//Txs Conf
			rk = RegTools.OpenOrCreateRegKey("Txs", true);
			rc.szTxs_LastPath = (string)rk.GetValue("szTxsLastPath", "");
			rc.szTxs_LastExportPath = (string)rk.GetValue("szTxsLastExportPath", "");
			rc.szTxs_LastImportPath = (string)rk.GetValue("szTxsLastImportPath", "");
			rc.szTxs_DefEditor = (string)rk.GetValue("szTxsDefEditor", "");
			rc.nTxs_DefThumbSize = (int)rk.GetValue("nTxsDefThumbSize", 64);
			rc.szTxs_DefImgType = (string)rk.GetValue("szTxsDefImgType", "gif");
			rc.nTxs_LastNewWidth = (int)rk.GetValue("nTxsLastNewWidth", 64);
			rc.nTxs_LastNewHeight = (int)rk.GetValue("nTxsLastNewHeight", 64);
			rc.nTxs_NewColor1 = (int)rk.GetValue("nTxsNewColor1", Color.Black.ToArgb());
			rc.nTxs_NewColor2 = (int)rk.GetValue("nTxsNewColor2", Color.Purple.ToArgb());
			rc.szVtf_LastBrowserPath = (string)rk.GetValue("szVtfLastBrowserPath", "");

			//Res Conf
			rk = RegTools.OpenOrCreateRegKey("Res", true);
			rc.szRes_LastPath = (string)rk.GetValue("szResLastPath", "");
			rc.bRes_CommentUnnecessaryFiles = ((int)rk.GetValue("bResCommentUnnecessary", 1) == 1) ? true : false;

			//Gcf Conf
			rk = RegTools.OpenOrCreateRegKey("Xpl", true);
			rc.szXpl_LastExtractPath = (string)rk.GetValue("szXplLastExtractPath", "");
			rc.szXpl_LastInputPath = (string)rk.GetValue("szXplLastInputPath", rc.szGen_GcfsPath);

			//Sys Conf
			rk = RegTools.OpenOrCreateRegKey("Sys", true);
			rc.bSys_AssociateBsp = ((int)rk.GetValue("bSysAssociateBsp", 0) == 1) ? true : false;
			rc.bSys_AssociatePck = ((int)rk.GetValue("bSysAssociatePck", 0) == 1) ? true : false;
			rc.bSys_AssociateTex = ((int)rk.GetValue("bSysAssociateTex", 0) == 1) ? true : false;
			rc.bSys_CheckAndRestore = ((int)rk.GetValue("bSysCheckAndRestore", 1) == 1) ? true : false;

			//Cln Conf
			rk = RegTools.OpenOrCreateRegKey("Cln", true);
			rc.nCln_LastMod = (int)rk.GetValue("nCln_LastMod", 0);

			//Down Conf
			rk = RegTools.OpenOrCreateRegKey("Dwn", true);
			rc.nDwn_DefAppId = (int)rk.GetValue("nDwnDefappID", 10);

			rk.Close();

			rc.szHlBoxTmpPath = Path.Combine(System.IO.Path.GetTempPath(), "~hlboxtemp");
			return rc;
			}
		/// <summary>
		/// Save configuration dans registre
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public void SaveMainConfig(stRegConf rc)
			{
			RegistryKey rk;

			rk = RegTools.OpenOrCreateRegKey("", true);
			//General Conf
			rk.SetValue("sz17bUrl", rc.szGen_17bUrl);
			rk.SetValue("sz17bUpdateUrl", rc.szGen_17bUpdateUrl);
			rk.SetValue("sz17bMInfosUrl", rc.szGen_17bMInfosUrl);
			rk.SetValue("szLanguage", rc.szGen_Language);
			rk.SetValue("szDLTarget", rc.szGen_DLTarget);
			rk.SetValue("szGenLastPath", rc.szGen_LastPath);
			rk.SetValue("bShowSplash", (rc.bGen_ShowSplash ? 1 : 0));
			rk.SetValue("bAskBeforeExit", (rc.bGen_AskBeforeExit ? 1 : 0));
			rk.SetValue("bCheckVerAtStartup", (rc.bGen_CheckVerAtStartup ? 1 : 0));
			rk.SetValue("bSimulate", (rc.bGen_Simulate ? 1 : 0));
			rk.SetValue("nGensHistorySize", rc.nGens_HistorySize);
			//Ims Conf
			rk = RegTools.OpenOrCreateRegKey("Pack", true);
			rk.SetValue("nPackOneForAll", rc.nPack_OneForAll);
			rk.SetValue("nPackEmptyPath", rc.nPack_EmptyPath);
			rk.SetValue("bPackCreateRes", (rc.bPack_CreateRes ? 1 : 0));
			rk.SetValue("bPackOverwriteRes", (rc.bPack_OverwriteRes ? 1 : 0));
			rk.SetValue("bPackIncludeWads", (rc.bPack_IncludeWads ? 1 : 0));
			rk.SetValue("szPackTargetPath", rc.szPack_TargetPath);
			rk.SetValue("szPackLastPath", rc.szPack_LastPath);
			rk.SetValue("bPackPakReplace", (rc.bPack_PakReplace ? 1 : 0));
			rk.SetValue("nPackHistorySize", rc.nPack_HistorySize);
			rk.SetValue("nPackZipLevel", rc.nPack_ZipLevel);
			//Smi Conf
			rk = RegTools.OpenOrCreateRegKey("Inst", true);
			rk.SetValue("bInstAnalysePath", (rc.bInst_AnalysePath ? 1 : 0));
			rk.SetValue("szInstMiscPath", rc.szInst_MiscPath);
			rk.SetValue("bInstAutoDetectFolder", (rc.bInst_AutoDetectFolder ? 1 : 0));
			rk.SetValue("bInstReplace", (rc.bInst_Replace ? 1 : 0));
			rk.SetValue("bInstAskWhenDiffers", (rc.bInst_AskWhenDiffers ? 1 : 0));
			rk.SetValue("szInstTargetPath", rc.szInst_TargetPath);
			rk.SetValue("szInstLastPath", rc.szInst_LastPath);
			rk.SetValue("nInstHistorySize", rc.nInst_HistorySize);
			//Qry Conf
			rk = RegTools.OpenOrCreateRegKey("Qry", true);
			rk.SetValue("szQryTargetPath", rc.szQry_TargetPath);
			rk.SetValue("nQryFileType", rc.nQry_FileType);
			rk.SetValue("nQryHistorySize", rc.nQry_HistorySize);

			//Ban Conf
			rk = RegTools.OpenOrCreateRegKey("Ban", true);
			rk.SetValue("szBanDefaultUrl", rc.szBan_defaultUrl);

			//Txs Conf
			rk = RegTools.OpenOrCreateRegKey("Txs", true);
			rk.SetValue("szTxsLastPath", rc.szTxs_LastPath);
			rk.SetValue("szTxsDefEditor", rc.szTxs_DefEditor);
			rk.SetValue("szTxsLastExportPath", rc.szTxs_LastExportPath);
			rk.SetValue("szTxsLastImportPath", rc.szTxs_LastImportPath);
			rk.SetValue("nTxsDefThumbSize", rc.nTxs_DefThumbSize);
			rk.SetValue("szTxsDefImgType", rc.szTxs_DefImgType);
			rk.SetValue("nTxsLastNewWidth",rc.nTxs_LastNewWidth);
			rk.SetValue("nTxsLastNewHeight",rc.nTxs_LastNewHeight);
			rk.SetValue("nTxsNewColor1", rc.nTxs_NewColor1);
			rk.SetValue("nTxsNewColor2", rc.nTxs_NewColor2);
			rk.SetValue("szVtfLastBrowserPath", rc.szVtf_LastBrowserPath);

			//Res Conf
			rk = RegTools.OpenOrCreateRegKey("Res", true);
			rk.SetValue("szResLastPath", rc.szRes_LastPath);
			rk.SetValue("bResCommentUnnecessary", (rc.bRes_CommentUnnecessaryFiles ? 1 : 0));

			//Gcf Conf
			rk = RegTools.OpenOrCreateRegKey("Xpl", true);
			rk.SetValue("szXplLastExtractPath", rc.szXpl_LastExtractPath);
			rk.SetValue("szXplLastInputPath", rc.szXpl_LastInputPath);

			//Cln Conf
			rk = RegTools.OpenOrCreateRegKey("Cln", true);
			rk.SetValue("nClnLastMod", rc.nCln_LastMod);

			//Sys Conf
			rk = RegTools.OpenOrCreateRegKey("Sys", true);
			rk.SetValue("bSysAssociateBsp", (rc.bSys_AssociateBsp ? 1 : 0));
			rk.SetValue("bSysAssociatePck", (rc.bSys_AssociatePck ? 1 : 0));
			rk.SetValue("bSysAssociateTex", (rc.bSys_AssociateTex ? 1 : 0));
			rk.SetValue("bSysCheckAndRestore", (rc.bSys_CheckAndRestore ? 1 : 0));

			//Down Conf
			rk = RegTools.OpenOrCreateRegKey("Dwn", true);
			rk.SetValue("nDwnDefappID", rc.nDwn_DefAppId);

			rk.Close();
			}

		}

}
