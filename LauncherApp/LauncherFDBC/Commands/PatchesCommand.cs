using BL_Launcher;
using BO_Launcher;
using LauncherFDBC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Commands
{
    public class PatchesCommand : BaseCommand
    {
        private readonly MainViewModel vm;
        public PatchesCommand(MainViewModel vm)
        {
            this.vm = vm;
        }

        public override void Execute(object parameter)
        {
            ProgramBO last = vm.Patches[vm.Patches.Count - 1];
            vm.Patches.AddRange(ProgramBL.GetPatchNote(last));
        }
    }
}
