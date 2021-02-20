﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeaponMaker
{
    public class OpenProjectCommand : Command
    {
        public OpenProjectCommand()
        {
        }

        public override bool Execute(object parameter = null)
        {
            var result = FileSystemService.OpenProject();
            if (result.success)
            {
                var session = ServiceLocator.Fetch<SessionService>();
                session.Project = result.project;
                return true;
            }
            return false;
        }
    }
}
