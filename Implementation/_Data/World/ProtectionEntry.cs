﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Terraria.Plugins.Common.Collections;
using DPoint = System.Drawing.Point;

using TShockAPI;

namespace Terraria.Plugins.CoderCow.Protector {
  public class ProtectionEntry {
    #region [Property: Owner]
    private int owner;

    public int Owner {
      get { return this.owner; }
    }
    #endregion

    #region [Property: TileLocation]
    private readonly DPoint tileLocation;

    public DPoint TileLocation {
      get { return this.tileLocation; }
    }
    #endregion

    #region [Property: TimeOfCreation]
    private DateTime timeOfCreation;

    public DateTime TimeOfCreation {
      get { return this.timeOfCreation; }
    }
    #endregion

    #region [Property: IsShared]
    public bool IsShared {
      get {
        return (
          this.IsSharedWithAll ||
          this.SharedUsers != null ||
          this.SharedGroups != null
        );
      }
    }
    #endregion

    #region [Property: IsSharedWithAll]
    private bool isSharedWithAll;

    public bool IsSharedWithAll {
      get { return this.isSharedWithAll; }
      set { this.isSharedWithAll = value; }
    }
    #endregion

    #region [Property: SharedUsers]
    private Collection<int> sharedUsers;

    public Collection<int> SharedUsers {
      get { return this.sharedUsers; }
      set { this.sharedUsers = value; }
    }
    #endregion

    #region [Property: SharedGroups]
    private StringCollection sharedGroups;

    public StringCollection SharedGroups {
      get { return this.sharedGroups; }
      set { this.sharedGroups = value; }
    }
    #endregion

    #region [Property: BankChestKey]
    private BankChestDataKey bankChestKey;

    public BankChestDataKey BankChestKey {
      get { return this.bankChestKey; }
      set { this.bankChestKey = value; }
    }
    #endregion

    #region [Property: RefillChestData]
    private RefillChestMetadata refillChestData;

    public RefillChestMetadata RefillChestData {
      get { return this.refillChestData; }
      set { this.refillChestData = value; }
    }
    #endregion


    #region [Method: Constructor]
    public ProtectionEntry(int owner, DPoint tileLocation) {
      this.owner = owner;
      this.tileLocation = tileLocation;
      this.timeOfCreation = DateTime.UtcNow;
    }

    protected ProtectionEntry() {}
    #endregion

    #region [Methods: IsSharedWithPlayer, Unshare]
    public bool IsSharedWithPlayer(TSPlayer player) {
      return (
        player.IsLoggedIn && (
          this.IsSharedWithAll ||
          (this.SharedUsers != null && this.SharedUsers.Contains(player.UserID)) || 
          (this.SharedGroups != null && this.SharedGroups.Contains(player.Group.Name))
        )
      );
    }

    public void Unshare() {
      this.sharedUsers = null;
      this.sharedGroups = null;
      this.isSharedWithAll = false;
    }
    #endregion

    #region [Method: ToString]
    public override string ToString() {
      return string.Format("{{Owner={0} TileLocation={1}}}", this.Owner, this.TileLocation);
    }
    #endregion
  }
}
