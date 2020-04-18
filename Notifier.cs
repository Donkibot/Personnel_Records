using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace WPF_MD_Personnel_Records
{
    public class Notifier : INotifyPropertyChanged
    {

        #region Fields
        SqlManager sqlManager;
        Member selectedMember;
        Member lastSelectedMember;
        Position selectedPosition;
        PhotoClass photoClass;
        Member createdMember;
        public ObservableCollection<Position> positions { get; set; }
        ObservableCollection<Member> Members;
        public List<Position> searchPositions { get; set; }

        #endregion

        #region Init
        public Notifier()
        {
            InitVariables();
            InitClassObjects();
            InitCommands();
            
        }

        private void InitVariables()
        {
            sqlManager = new SqlManager();
            positions = new ObservableCollection<Position>(new Position().GetPositionsListFromDB());
            searchPositions = new List<Position>(positions);
            searchPositions.Add(new Position(0,"Всі"));
        }

        void InitClassObjects()
        {
            Members = new ObservableCollection<Member>(new Member().GetMembers());
            photoClass = new PhotoClass(null);
            createdMember = new Member();
        }

        void InitCommands()
        {
            ChangeIsReadOnly = new DelegateCommand(ChangeIsReadOnlytatus);
            LoadPhoto = new DelegateCommand(_LoadPhoto);
            DeleteMember = new DelegateCommand(_DeleteMember);
            ShowMemberBuilder = new DelegateCommand(_ShowMemberBuilder);
            HideMemberBuilder = new DelegateCommand(_HideMemberBuilder);
            AddMemberToDB = new DelegateCommand(_AddMemberToDB, CandAddNewMember);
            SetGender = new DelegateCommand(_SetGender);
        }

       
        #endregion

        #region ICommands
        public ICommand ChangeIsReadOnly { get; set; }
        public ICommand LoadPhoto { get; set; }
        public ICommand DeleteMember { get; set; }
        public ICommand ShowMemberBuilder { get; set; }
        public ICommand HideMemberBuilder { get; set; }
        public ICommand AddMemberToDB { get; set; }
        public ICommand SetGender { get; set; }
        #endregion

        #region CommandsRealization
        private void _AddMemberToDB(object obj)
        {
            createdMember.photo = photoClass.GetInstance(lastPathFromFileDialog);
            sqlManager.AddMember(createdMember);
            UpdateMembersList();
            lastPathFromFileDialog = null;
            BuilderPageVisibility = "Hidden";
        }

        private void _HideMemberBuilder(object obj)
        {
            BuilderPageVisibility = "Hidden";
        }
        private void _ShowMemberBuilder(object obj)
        {
            BuilderPageVisibility = "Visible";
            CreatedMember = new Member();
        }
        private void _DeleteMember(object obj)
        {
            sqlManager.DeleteMember(LastSelectedMember.id);
            LastSelectedMember = new Member() {photo = new PhotoClass(null), position = new Position(), sex = null };
            UpdateMembersList();
        }

        private void _LoadPhoto(object obj)
        {
            BitmapImage tempImage = photoClass.GetPhotoFromPath(GetPathFromFileDialogo());
            if ((string)obj == "MainViewFrame")
            {
                DisplayedImageInMainView = tempImage;
            }
            else if ((string)obj == "CreatorPage")
            {
                DisplayedImageInCreatorPage = tempImage;
            }
        }

        private void ChangeIsReadOnlytatus(object obj)
        {
            if (IsReadOnly == true)
            {
                IsReadOnly = false;
            }
            else
            {
                IsReadOnly = true;
                UpdateMemberInSql();
            }
            ButtonVisibility = "";
            BtnOnovityContent = "";
            PositionTBVisibility = "";
        }

        bool CandAddNewMember(object obj)
        {
            //bool isValid = !createdMember.errors.Values.Any(x=> x != null);
            bool isValid = !createdMember.IsAnyOfNeededFieldsIsNull();
            return isValid;
        }

        private void _SetGender(object obj)
        {
            createdMember.sex = (string)obj;
        }
        #endregion



        public ObservableCollection<Member> members
        {
            get => Members;
            set
            {
                Members = value;
                Notify("members");
            }
        }
      


        string builderPageVisibility = "Hidden";
        public string BuilderPageVisibility
        {
            get => builderPageVisibility;
            set
            {
                builderPageVisibility = value;
                Notify("BuilderPageVisibility");
            }
        }

        string lastPathFromFileDialog;
        string GetPathFromFileDialogo()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.txt)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            fileDialog.ShowDialog();
            lastPathFromFileDialog = fileDialog.FileName;
            return lastPathFromFileDialog;
        }

        void UpdateMemberInSql()
        {
            if (lastPathFromFileDialog != null)
                LastSelectedMember.photo = photoClass.GetInstance(lastPathFromFileDialog);
            sqlManager.updateMember(LastSelectedMember);
            lastPathFromFileDialog = null;
            UpdateMembersList();
        }

        void UpdateMembersList()
        {
            members = new ObservableCollection<Member>(new Member().GetMembers());
        }
         public void FilterMemberList(List<Member> memberFound)
        {
            members = new ObservableCollection<Member>(memberFound);
        }

        public Member SelectedMember
        {
            get => selectedMember;
            set
            {
                selectedMember = value;
                if (SelectedMember != null)
                {
                    LastSelectedMember = value.GetCopy();
                    Notify("SelectedMember");
                }
            }
        }

        public Member LastSelectedMember
        {
            get => lastSelectedMember;
            set
            {
                lastPathFromFileDialog = null;
                lastSelectedMember = value;
                DisplayedImageInMainView = value.photo.GetPhoto();
                SetSelectedPosition(value);
                canEdit = true;
                Notify("LastSelectedMember");
            }
        }

        public Member CreatedMember
        {
            get => createdMember;
            set
            {
                createdMember = value;
                Notify("CreatedMember");
            }
        }

        void SetSelectedPosition(Member lastSelectedMember)
        {
            foreach (Position p in positions)
            {
                if (p.id == lastSelectedMember.position.id)
                {
                    SelectedPosition = p;
                }
            }
        }

        public Position SelectedPosition
        {
            get => selectedPosition;
            set
            {
                selectedPosition = value;
                LastSelectedMember.position = value;
                if (selectedPosition != null)
                {
                    Notify("SelectedPosition");
                }
            }
        }


        public bool canEdit
        {
            get { return _canEdit; }
            set
            {
                _canEdit = value;
                Notify("canEdit");
            }
        }

        private bool _canEdit = false;

        string testString;
        public string TestString
        {
            get => testString;
            set
            {
                testString = value;
                Notify(TestString);
            }
        }

        bool isReadOnly = true;
        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                Notify("IsReadOnly");
            }
        }

        string buttonVisibility = "Hidden";
        public string ButtonVisibility
        {
            get => buttonVisibility;
            set
            {
                buttonVisibility = isReadOnly ? "Hidden" : "Visible";
                Notify("ButtonVisibility");
            }
        }

        string _btnOnovityContent = "Оновини";

        public string BtnOnovityContent
        {
            get => _btnOnovityContent;
            set
            {
                _btnOnovityContent = isReadOnly ? "Оновини" : "Підтвердити";
                Notify("BtnOnovityContent");
            }
        }

        string _positionTBVisibility = "Visible";
        public string PositionTBVisibility
        {
            get => _positionTBVisibility;
            set
            {
                _positionTBVisibility = isReadOnly ? "Visible" : "Hidden";
                Notify("PositionTBVisibility");
            }
        }


        ImageSource displayedImageInMainView;
        public ImageSource DisplayedImageInMainView
        {
            get
            {
                if (LastSelectedMember != null)
                {
                    return displayedImageInMainView;
                }
                return photoClass.GetPhoto();
            }
            set
            {
                displayedImageInMainView = value;
                Notify("DisplayedImageInMainView");
            }
        }

        ImageSource displayedImageInCreatorPage;
        public ImageSource DisplayedImageInCreatorPage
        {
            get
            {
                if (lastPathFromFileDialog != null)
                {
                    return displayedImageInCreatorPage;
                }
                return photoClass.GetPhoto();
            }
            set
            {
                displayedImageInCreatorPage = value;
                Notify("DisplayedImageInCreatorPage");
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
