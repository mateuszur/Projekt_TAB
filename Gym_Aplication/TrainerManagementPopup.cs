using System;
using System.ComponentModel;
using System.Windows.Data;

namespace Gym_Aplication
{
    internal class TrainerManagementPopup
    {
        private TrainerManagement trainerManagement;

        public TrainerManagementPopup(TrainerManagement trainerManagement)
        {
            this.trainerManagement = trainerManagement;
        }

        public void ShowTrainerManagementPopup()
        {
            var popup = new TrainerManagementPopup(trainerManagement);
            popup.ShowDialog();

            ICollectionView view = CollectionViewSource.GetDefaultView(trainerManagement.trainerList);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            view.Filter = obj =>
            {
                TrainerManagement trainer = obj as TrainerManagement;
                return trainer != null && trainer.Name.StartsWith("A");
            };
        }

        private void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}