﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetEventer
{
    public class Event
    {
        List<EventField> _fields = new List<EventField>();
        private DateTime _timeStamp = DateTime.Now;
        

        public Event()
        {
        
        }

        /// <summary>
        /// List of event fields
        /// </summary>
        public List<EventField> Fields
        {
            get
            {
                return _fields;
            }
        }

        /// <summary>
        /// Field indexer
        /// </summary>
        /// <param name="FieldName">Name of the field to look for</param>
        /// <returns>An EventField if found or null if not</returns>
        public EventField this[string FieldName]
        {
            get
            {
                int _fieldIndex = _fields.FindIndex(f => f.Name == FieldName);

                if (_fieldIndex != -1)
                {
                    return _fields[_fieldIndex];
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Event timestamp
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }


        /// <summary>
        /// Render a full event in Splunk format
        /// </summary>
        /// <returns>A string representing the event in a Splunk format</returns>
        public string Render()
        {
            
            StringBuilder _sb = new StringBuilder();
            
            _sb.Append(string.Format("{0:o} ", _timeStamp));
            
             

            //Append every existing fields
            foreach (DotNetEventer.EventField _field in Fields)
            {
                _sb.Append(string.Format("{0}={1} ", _field.Name, FormatValue(_field)));
            }

            return _sb.ToString().TrimEnd();
        }


        /// <summary>
        /// Format a "EventField" value
        /// </summary>
        /// <param name="EventField">The EventField to format</param>
        /// <returns>A string representing a formated value</returns>
        private string FormatValue(DotNetEventer.EventField EventField)
        {
            if (EventField.IsNumber)
            {
                return EventField.Value;
            }
            else
            {
                if (EventField.Value.Contains(" "))
                {
                    return string.Format("\"{0}\"", EventField.Value);
                }
                else
                {
                    return EventField.Value;
                }
            }
        }

        

    }
}