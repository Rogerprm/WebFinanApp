using MudBlazor;

namespace AppPrimiani.Web
{
    public static class Configuration
    {
        public static MudTheme Theme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#3f51b5",
                Secondary = "#f50057",
                Background = "#f5f5f5",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#3f51b5",
                Secondary = "#f50057",
                Background = "#333",
            }
        };
    }
}
