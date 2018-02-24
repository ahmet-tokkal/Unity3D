private static var obje : SingletonMuzik = null;
 
function Awake()
{
    if( obje == null )
    {
        obje = this;
        DontDestroyOnLoad(this);
    }
    else if( this != obje )
    {
        Destroy( gameObject );
    }
}
function OnLevelWasLoaded( level : int )
    {
        if( Application.loadedLevelName != "0-Menu" && Application.loadedLevelName != "1-1-0-HarfPatlat" &&
            Application.loadedLevelName != "1-0-Bolumler" && Application.loadedLevelName !="1-2-0-KelimePatlat" &&
            Application.loadedLevelName !="1-3-0-SoruPatlat" && Application.loadedLevelName !="1-4-0-IngilizcePatlat" &&
            Application.loadedLevelName !="1-5-0-UlkePatlat" && Application.loadedLevelName !="1-6-0-UnluPatlat"&&
            Application.loadedLevelName !="2-Yardım")
        {
            obje = null;
            Destroy( gameObject );
        }
    }   