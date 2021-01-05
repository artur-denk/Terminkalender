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

namespace Terminkalender.Model.Appointments
{
    /// <summary>
    /// This class represents an appointment.
    /// </summary>
    internal class Appointment
    {
        /// <summary>
        /// Gets the unique id of the appointment.
        /// </summary>
        internal string ID { get; private set; }

        /// <summary>
        /// Gets or sets the title of the appointment.
        /// </summary>
        internal string Title { get; set; }

        /// <summary>
        /// Gets or sets the start date of the appointment.
        /// </summary>
        internal DateTime DateStart { get; set; }

        /// <summary>
        /// Gets or sets the end date of the appointment.
        /// </summary>
        internal DateTime DateEnd { get; set; }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="id">The unique id of the appointment.</param>
        /// <param name="title">The title of the appointment.</param>
        /// <param name="dateStart">The start date of the appointment.</param>
        /// <param name="dateEnd">The end date of the appointment.</param>
        internal Appointment(string id, string title, DateTime dateStart, DateTime dateEnd)
        {
            ID = id;
            Title = title;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
    }
}
