using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPublisher
{
    public enum Events
    {
        RowSelectionChange,
        SetRightTextBoxText,
        MoveToPriviousRecord,
        MoveToNextRecord,
        ReOrder,
        RecordUpdated,
        Save,
        WordDocClosed,
        WordDocSaved,
        OpenWord,
        DescriptionCount,
        ShowFilter,
        ShowFilterDone,
        DataImportSelectionCompleted,
        StartDataImport,
        Undo,
        ReDo
    }
}
