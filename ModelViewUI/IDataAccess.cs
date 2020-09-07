using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewUI
{
    public interface IDataAccess : IPCVCUIService
    {
        //UserManagementResultInfo ValidateUser(UserManagementInputInfo userManagementInputInfo);
        //LanguageResultInfo FillLanguage();
        //CaptionMasterResultInfo FillCaption(CaptionMasterInputInfo captionMasterInputInfo);
        //FieldMasterInfo FillField(FieldMasterInputInfo fieldMasterInputInfo);


        //ScreenSequencResultInfo GetScreenSequence(ScreenSequencInputInfo screenSequencInputInfo);
        //AudioScriptResultInfo GetScriptSequence(AudioScriptInputInfo audioScriptInputInfo);
        //AudioMasterResulInfo FillAudioMaster(AudioMasterInputInfo audioMasterInputInfo);

        //IncompletePCVCOutPutInfo FillIncompleteList();

        //bool SavePropDataMaster(PraposalDetailResultInfo PropResult);
        //bool SaveLanguageMaster(List<LanguageInfo> langResult);
        //bool SaveFeildMaster(List<FieldMaster> FeildInfo);
        //bool SaveCaptionMaster(List<CaptionMaster> Captions);
        //bool UpdatePCVCLog(UploadResultInfo UplodResult);
        //bool SaveUserMaster(UserManagementResultInfo userinfo);
        //bool SaveProdMaster(List<product> prod);
        //bool SaveScreenMaster(List<ScreenDownload> screenDownload);
        //bool SaveProdGroupMaster(List<productGroup> productgr);

        //Tuple<bool, List<AudioDownloadResumeInfo>> SaveAudioMaster(List<AudioDownloadInfo> audioinfo);
        //bool SavescriptMapping(List<scriptMapDownload> scriptMap);
        //bool SavescriptDeata(List<ScriptDetaDownload> scriptDeata);
        //bool SaveScriptMast(List<ScriptMastDowload> ScriptMast);
        //bool SaveprodGrpScrnMap(List<prodGrpScrnMapMast> scriptMap);
        //bool UpdatePropDataMaster(PraposalDetailResultInfo PropResult);
        //int UpdateDownloadAudio(AudioDownloadInfo audioDownloadInfo);

        //bool UpdateLanguageMaster(DownloadLangDate langResult);
        //DownloadLangDate GetLangDownloadStatus(int langid);
        //bool UpdateScriptToDB(string qr);
        //string GetDBVersion();
        //bool CleanTable(string DbVersion);
    }

}
