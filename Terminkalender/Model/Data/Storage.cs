/*
    Copyright (c) 2021 Artur Denk
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Terminkalender.Model.Appointments;

namespace Terminkalender.Model.Data
{
    /// <summary>
    /// This class serializes and deserializes appointments.
    /// </summary>
    internal class Storage
    {
        /// <summary>
        /// Gets the file path location.
        /// </summary>
        private string FilePath
        {
            get
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                string fileName = "appointments.xml";

                string filePath = $"{projectDirectory}\\{fileName}";
                return filePath;
            }
        }

        /// <summary>
        /// Saves all appointments to the XML file.
        /// </summary>
        /// <param name="appointments">The list of all appointments.</param>
        /// <returns>True if saving was successful; otherwise false.</returns>
        internal bool SaveAppointments(List<Appointment> appointments)
        {
            XDocument xDoc = new XDocument 
            { 
                Declaration = new XDeclaration("1.0", "UTF-8", null)
            };

            xDoc.Add(new XElement("appointments", 
                appointments.Select(appointment =>
                    new XElement("appointment", 
                        new XAttribute("id", appointment.ID),
                        new XElement("title", appointment.Title),
                        new XElement("dateStart", appointment.DateStart.Ticks),
                        new XElement("dateEnd", appointment.DateEnd.Ticks)
                    )
                )
            ));

            try
            {
                xDoc.Save(FilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads all appointments if an XML file exists.
        /// </summary>
        /// <returns>The list of all loaded appointments.</returns>
        internal List<Appointment> LoadAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            if (File.Exists(FilePath))
            {
                XDocument xml = XDocument.Load(FilePath);
                
                try
                {
                    foreach (var appointmentNode in xml.Root.Elements())
                    {
                        string id = appointmentNode.Attribute("id").Value;
                        string title = appointmentNode.Element("title").Value;

                        string dateStartTicksValue = appointmentNode.Element("dateStart").Value;
                        long dateStartTicks = long.Parse(dateStartTicksValue);
                        DateTime dateStart = new DateTime(dateStartTicks);

                        string dateEndTicksValue = appointmentNode.Element("dateEnd").Value;
                        long dateEndTicks = long.Parse(dateEndTicksValue);
                        DateTime dateEnd = new DateTime(dateEndTicks);

                        Appointment appointment = new Appointment(id, title, dateStart, dateEnd);
                        appointments.Add(appointment);
                    }
                }
                catch
                {
                    appointments = new List<Appointment>();
                }
            }

            return appointments;
        }
    }
}
