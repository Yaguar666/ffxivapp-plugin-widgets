﻿// FFXIVAPP.Plugin.Widgets
// XIVInfoViewModel.cs
// 
// Copyright © 2007 - 2015 Ryan Wilson - All Rights Reserved
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution. 
//  * Neither the name of SyndicatedLife nor the names of its contributors may 
//    be used to endorse or promote products derived from this software 
//    without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using FFXIVAPP.Common.Core.Memory;

namespace FFXIVAPP.Plugin.Widgets.ViewModels
{
    public class XIVInfoViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static XIVInfoViewModel _instance;
        private ActorEntity _currentUser;
        private ConcurrentDictionary<UInt32, ActorEntity> _currentMonsters;
        private ConcurrentDictionary<uint, ActorEntity> _currentNPCs;
        private ConcurrentDictionary<uint, ActorEntity> _currentPCs;

        public static XIVInfoViewModel Instance
        {
            get { return _instance ?? (_instance = new XIVInfoViewModel()); }
            set { _instance = value; }
        }

        public ActorEntity CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<uint, ActorEntity> CurrentNPCs
        {
            get { return _currentNPCs ?? (_currentNPCs = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentNPCs = value;
                if (value.Any())
                {
                    CurrentUser = value.FirstOrDefault()
                                       .Value.CurrentUser;
                }
                RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<UInt32, ActorEntity> CurrentMonsters
        {
            get { return _currentMonsters ?? (_currentMonsters = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentMonsters = value;
                if (value.Any())
                {
                    CurrentUser = value.FirstOrDefault()
                                       .Value.CurrentUser;
                }
                RaisePropertyChanged();
            }
        }

        public ConcurrentDictionary<uint, ActorEntity> CurrentPCs
        {
            get { return _currentPCs ?? (_currentPCs = new ConcurrentDictionary<uint, ActorEntity>()); }
            set
            {
                _currentPCs = value;
                if (value.Any())
                {
                    CurrentUser = value.FirstOrDefault()
                                       .Value.CurrentUser;
                }
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        public readonly Timer InfoTimer = new Timer(100);

        public bool IsProcessing { get; set; }

        #endregion

        public XIVInfoViewModel()
        {
            InfoTimer.Elapsed += InfoTimerOnElapsed;
            //InfoTimer.Start();
        }

        private void InfoTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (IsProcessing)
            {
                return;
            }
            IsProcessing = true;
            // do stuff if you have too
            IsProcessing = false;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
