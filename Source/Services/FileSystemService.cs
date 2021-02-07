﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeaponMaker
{
    public class FileSystemService
    {
        public static (bool success, Project project) OpenProject()
        {
            (bool success, Project project)result = (false, null);
            var fileDialog = new OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".wmprj",
                Filter = "Weapon Maker Project files(*.wmprj) | *.wmprj"
            };
            var dialogResult = fileDialog.ShowDialog();
            switch (dialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    string input = "";
                    using (StreamReader sw = new StreamReader(fileDialog.FileName))
                    {
                        input = sw.ReadToEnd();
                    }
                    var project = JsonConvert.DeserializeObject<Project>(input);
                    result.success = true;
                    result.project = project;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }

            return result;
        }

        public static bool SaveProject(Project project)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = project.Name,
                DefaultExt = ".wmprj",
                Filter = "Weapon Maker Project files (*.wmprj)|*.wmprj"
            };
            var dialogResult = fileDialog.ShowDialog();
            var success = false;
            switch (dialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    try
                    {
                        string output = JsonConvert.SerializeObject(project, Formatting.Indented);
                        using (StreamWriter sw = new StreamWriter(fileDialog.FileName))
                        {
                            sw.WriteLine(output);
                        }
                        System.Windows.MessageBox.Show($"Saved {project.Name}!", "Success");
                        success = true;
                    }
                    catch (Exception exception)
                    {
                        System.Windows.MessageBox.Show($"{exception.Message}", "Error");
                    }
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
            return success;
        }

        public static void ExportWeapon(Weapon weapon)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = weapon.Name,
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };
            var dialogResult = fileDialog.ShowDialog();
            switch (dialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    try
                    {
                        string output = JsonConvert.SerializeObject(weapon, Formatting.Indented);
                        using (StreamWriter sw = new StreamWriter(fileDialog.FileName))
                        {
                            sw.WriteLine(output);
                        }
                        System.Windows.MessageBox.Show($"Sucess exporting {weapon.Name}!", "Success");
                    }
                    catch (Exception exception)
                    {
                        System.Windows.MessageBox.Show($"{exception.Message}", "Error");
                    }
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }

        public static (bool success, Weapon weapon) ImportWeapon()
        {
            (bool success, Weapon weapon) result = (false, null);
            var fileDialog = new OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json"
            };
            var dialogResult = fileDialog.ShowDialog();
            switch (dialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    string input = "";
                    using (StreamReader sw = new StreamReader(fileDialog.FileName))
                    {
                        input = sw.ReadToEnd();
                    }
                    var weapon = JsonConvert.DeserializeObject<Weapon>(input);
                    result.weapon = weapon;
                    result.success = true;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
            return result;
        }
    }

}