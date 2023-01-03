using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using HLBox17b.Externals;

namespace HLBox17b
	{
	public class myTreeListItems
		{
		public List<TreeItems> TrItems;
		public myTreeListItems()
			{
			TrItems = new List<TreeItems>();
			}
		// Ajouter un Noeud à la liste
		public int Add2Tree(int ParentId, string szFile, bool IsDir,int ForcedStatus)
			{
			int NewID = 0;
			int TmpId = -1;
			int NumItms = TrItems.Count + 1;
			string RegPat = @"[\\/]";
			int Stus = 0;

			Regex r = new Regex(RegPat, RegexOptions.IgnoreCase);

			Match m = r.Match(szFile);
			if (m.Success) //On a trouvé un anti-slash: le fichier possède un path
				{
				int nPos = m.Index; //On cherche la position de l'antislash
				if (nPos != 0) // Il n'est pas au début de la chaine: on a un path
					{
					string NodeKey = szFile.Substring(0, nPos);
					TmpId = this.IsInParent(ParentId, NodeKey);
					if (TmpId == -1) // Le Noeud n'existe pas chez le Parent
						{
						Stus = 0;
						TrItems.Add(new TreeItems(NumItms, ParentId, NodeKey, Stus, szFile, true));
						TmpId = NumItms;
						}
					NewID = this.Add2Tree(TmpId, szFile.Substring(nPos + 1),false,ForcedStatus);
					}
				else //l'anti-slah est en début de chaine: on en tient pas compte
					{
					NewID = this.Add2Tree(ParentId, szFile.Substring(nPos + 1),false,ForcedStatus);
					}
				}
			else // Le fichier ne contient pas de path
				{
				if ((this.IsInParent(ParentId, szFile)) != -1)
					Stus = 1;
				if (ForcedStatus != -1)
					Stus = ForcedStatus;
				if (ParentId == 0)
					IsDir = true;
				else
					IsDir = false;
				TrItems.Add(new TreeItems(NumItms, ParentId, szFile, Stus, szFile,IsDir));
				NewID = NumItms;
				if (Stus == 1)
					SetError(GetRootParent(NewID),2);
				if (Stus == 4)
					SetError(GetRootParent(NewID), 3);
				}
			return NewID;
			}

		public int GetRootParent(int NewID)
			{
			TreeItems tritm = TrItems.ElementAt(NewID - 1);

			if (tritm.ParentID == 0)
				return tritm.ID;
			return GetRootParent(tritm.ParentID);
			}
		//Vérifier si un noeud existe deja pour le même parent
		public int IsInParent(int ParentId, string NodeKey)
			{
			int ExistingId = -1;
			foreach (TreeItems tritm in TrItems)
				{
				if (tritm.ParentID == ParentId && tritm.NodeText == NodeKey)
					{
					ExistingId = tritm.ID;
					break;
					}
				}
			return ExistingId;
			}
		//Ajouter la liste à un controle treeview
		public void Dump2TreeView(TreeView TrView, bool bSort, bool bExpand)
			{
			Cursor.Current = Cursors.WaitCursor;
			TrView.Nodes.Clear();    // Clear any existing items            
			TrView.BeginUpdate();    // prevent overhead and flicker            
			this.AddNodes(TrView);    // load all the lowest tree nodes            
			TrView.EndUpdate();      // re-enable the tree            
			if (bSort)
				TrView.Sort();
			if (bExpand)
				ExpandBugged(TrView);
				//TrView.ExpandAll();
			TrView.Refresh();
			Cursor.Current = Cursors.Default;
			}


		public void ExpandBugged(TreeView TrView)
			{
			foreach (TreeItems tritm in TrItems)
				{
				if (tritm.ParentID == 0)
					{
					TreeNode TmpNode = new TreeNode(); ;
					TmpNode.Tag = tritm;
					TmpNode.ForeColor = this.GetNodeColor(tritm.Status);
					TmpNode.ImageIndex = tritm.NodeImg;
					TmpNode.SelectedImageIndex = tritm.NodeImg;
					}
				}
			}
		//Débuter le chargement des Noeuds
		private void AddNodes(TreeView TrView)
			{
			foreach (TreeItems tritm in TrItems)
				{
				if (tritm.ParentID == 0)
					{
					TreeNode TmpNode = new TreeNode(); ;
					TmpNode.Tag = tritm;
					TmpNode.ForeColor = this.GetNodeColor(tritm.Status);
					TmpNode.ImageIndex = tritm.NodeImg;
					TmpNode.SelectedImageIndex = tritm.NodeImg;
					TmpNode.Text = tritm.NodeText;
					int nIdx = TrView.Nodes.Add(TmpNode);
					TreeNode NewNode = TrView.Nodes[nIdx];
					this.getChildrens(NewNode);
					}
				}
			return;
			}

		public void SetError(int TrID,int ErrorType)
			{
			foreach (TreeItems tritm in TrItems)
				{
				if (tritm.ID == TrID)
					{
					if (ErrorType == 3)
						{
						if (tritm.NodeImg != 2 && tritm.NodeImg != 4)
							tritm.NodeImg = ErrorType;
						}
					else
						{
						tritm.NodeImg = ErrorType;
						}
					break;
					}
				}
			return;
			}
		//parcourir et créer les enfants d'un noeud
		private bool getChildrens(TreeNode node)
			{
			bool bstatus = true;
			TreeItems nodeCat = (TreeItems)node.Tag;  // get the category for this node
			foreach (TreeItems tritm in TrItems)
				{
				if (tritm.ParentID == nodeCat.ID)     // found a child
					{
					TreeNode TmpNode = new TreeNode();
					TmpNode.Tag = tritm;
					TmpNode.ForeColor = this.GetNodeColor(tritm.Status);
					TmpNode.Text = tritm.NodeText;
					TmpNode.ImageIndex = tritm.NodeImg;
					TmpNode.SelectedImageIndex = tritm.NodeImg;
					int nIdx = node.Nodes.Add(TmpNode);
					TreeNode Node = node.Nodes[nIdx];					
					bstatus = getChildrens(Node);                      // find this child's children
					}
				}
			return bstatus;
			}
		//Couleur d'un item en fonction du status
		private Color GetNodeColor(int Status)
			{
			switch (Status)
				{
				case 4:
					return Color.LightCoral;
				case 3:
					return Color.DarkGreen;
				case 2:
					return Color.Blue;
				case 1:
					return Color.Red;
				default:
					return Color.Black;
				}
			}
		}

	public class TreeItems
		{
		public int ID;
		public int ParentID;
		public string NodeText;
		public int Status;
		public int NodeImg;
		public string Extra;
		public bool isDir;

		public TreeItems(int ID, int ParentID, string NodeText, int Status, string Extra,bool IsDir)
			{
			this.ID = ID;
			this.ParentID = ParentID;
			this.NodeText = NodeText;
			this.Status = Status;
			if (ParentID == 0)
				this.NodeImg = 1;
			else
				this.NodeImg = ocTreeView.NOIMAGE;
			this.Extra = Extra;
			this.isDir = IsDir;
			}
		}
	}