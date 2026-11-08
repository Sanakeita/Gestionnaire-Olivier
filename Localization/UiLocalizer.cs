using System.Globalization;

namespace GestionOliviers.Localization;

public sealed class UiLocalizer
{
    private static readonly IReadOnlyDictionary<string, string[]> Translations =
        new Dictionary<string, string[]>
        {
            ["Gestion des champs"] = ["Field management", "إدارة الحقول", "农场管理"],
            ["Accueil"] = ["Home", "الرئيسية", "首页"],
            ["Champs"] = ["Fields", "الحقول", "地块"],
            ["Récoltes"] = ["Harvests", "المحاصيل", "收成"],
            ["Olivier"] = ["Olive tree", "شجرة الزيتون", "橄榄树"],
            ["Évolution"] = ["Trends", "التطور", "趋势"],
            ["Suivi des champs d’oliviers"] = ["Olive grove tracking", "متابعة حقول الزيتون", "橄榄园管理"],
            ["Chaque olivier produit une récolte par an. Enregistrez vos champs, vos arbres et leurs productions, puis visualisez l’évolution des récoltes par champ sur une période donnée."]
                = ["Each olive tree produces one harvest per year. Record your fields, trees and yields, then view harvest trends by field over a selected period.",
                    "تنتج كل شجرة زيتون محصولاً واحداً سنوياً. سجّل الحقول والأشجار والمحاصيل، ثم اعرض تطور المحاصيل حسب الحقل خلال فترة محددة.",
                    "每棵橄榄树每年收获一次。记录地块、树木和产量，并按所选时间段查看各地块的收成趋势。"],
            ["Total récolté"] = ["Total harvested", "إجمالي المحصول", "收获总量"],
            ["Nouveau champ"] = ["New field", "حقل جديد", "新建地块"],
            ["Saisir une récolte"] = ["Record a harvest", "تسجيل محصول", "记录收成"],
            ["Voir l’évolution"] = ["View trends", "عرض التطور", "查看趋势"],
            ["Parcelles d’oliviers suivies dans l’exploitation."] = ["Olive plots managed by the farm.", "قطع الزيتون التي تديرها المزرعة.", "农场管理的橄榄地块。"],
            ["Nom"] = ["Name", "الاسم", "名称"],
            ["Localisation"] = ["Location", "الموقع", "位置"],
            ["Superficie"] = ["Area", "المساحة", "面积"],
            ["Superficie (ha)"] = ["Area (ha)", "المساحة (هكتار)", "面积（公顷）"],
            ["Oliviers"] = ["Olive trees", "أشجار الزيتون", "橄榄树"],
            ["Détail"] = ["Details", "التفاصيل", "详情"],
            ["Modifier"] = ["Edit", "تعديل", "编辑"],
            ["Supprimer"] = ["Delete", "حذف", "删除"],
            ["Nouvel olivier"] = ["New olive tree", "شجرة زيتون جديدة", "新建橄榄树"],
            ["Chaque arbre produit une récolte par an."] = ["Each tree produces one harvest per year.", "تنتج كل شجرة محصولاً واحداً سنوياً.", "每棵树每年收获一次。"],
            ["Champ"] = ["Field", "الحقل", "地块"],
            ["Tous les champs"] = ["All fields", "كل الحقول", "所有地块"],
            ["Filtrer"] = ["Filter", "تصفية", "筛选"],
            ["Identifiant"] = ["Identifier", "المعرّف", "编号"],
            ["Variété"] = ["Variety", "الصنف", "品种"],
            ["Plantation"] = ["Planted", "سنة الغرس", "种植年份"],
            ["Année de plantation"] = ["Planting year", "سنة الغرس", "种植年份"],
            ["Modifier l’olivier"] = ["Edit olive tree", "تعديل شجرة الزيتون", "编辑橄榄树"],
            ["Enregistrer"] = ["Save", "حفظ", "保存"],
            ["Annuler"] = ["Cancel", "إلغاء", "取消"],
            ["planté en"] = ["planted in", "غُرست سنة", "种植于"],
            ["Ajouter une récolte"] = ["Add a harvest", "إضافة محصول", "添加收成"],
            ["Année"] = ["Year", "السنة", "年份"],
            ["Quantité"] = ["Quantity", "الكمية", "数量"],
            ["Date"] = ["Date", "التاريخ", "日期"],
            ["Qualité"] = ["Quality", "الجودة", "质量"],
            ["Supprimer l’olivier"] = ["Delete olive tree", "حذف شجرة الزيتون", "删除橄榄树"],
            ["Les récoltes de cet olivier seront également supprimées."] = ["This tree's harvest records will also be deleted.", "سيتم حذف سجلات محصول هذه الشجرة أيضاً.", "此橄榄树的收成记录也将被删除。"],
            ["Confirmer la suppression"] = ["Confirm deletion", "تأكيد الحذف", "确认删除"],
            ["Nom du champ"] = ["Field name", "اسم الحقل", "地块名称"],
            ["Modifier le champ"] = ["Edit field", "تعديل الحقل", "编辑地块"],
            ["Notes"] = ["Notes", "ملاحظات", "备注"],
            ["Ajouter un olivier"] = ["Add an olive tree", "إضافة شجرة زيتون", "添加橄榄树"],
            ["Supprimer le champ"] = ["Delete field", "حذف الحقل", "删除地块"],
            ["Les oliviers et les récoltes associés seront également supprimés."] = ["Associated olive trees and harvest records will also be deleted.", "سيتم حذف أشجار الزيتون والمحاصيل المرتبطة أيضاً.", "相关橄榄树和收成记录也将被删除。"],
            ["Une seule récolte par olivier et par année."] = ["Only one harvest per olive tree per year.", "محصول واحد فقط لكل شجرة زيتون في السنة.", "每棵橄榄树每年只能记录一次收成。"],
            ["Nouvelle récolte"] = ["New harvest", "محصول جديد", "新建收成"],
            ["Toutes les années"] = ["All years", "كل السنوات", "所有年份"],
            ["Quantité (kg)"] = ["Quantity (kg)", "الكمية (كغ)", "数量（千克）"],
            ["Date de récolte"] = ["Harvest date", "تاريخ الحصاد", "收获日期"],
            ["Supprimer la récolte"] = ["Delete harvest", "حذف المحصول", "删除收成"],
            ["Modifier la récolte"] = ["Edit harvest", "تعديل المحصول", "编辑收成"],
            ["Les champs suivis dans l’exploitation."] = ["Fields managed by the farm.", "الحقول التي تديرها المزرعة.", "农场管理的地块。"],
            ["Évolution des récoltes"] = ["Harvest trends", "تطور المحاصيل", "收成趋势"],
            ["Production annuelle agrégée par champ, pour la période choisie."] = ["Annual production by field for the selected period.", "الإنتاج السنوي حسب الحقل للفترة المحددة.", "所选时间段内各地块的年度产量。"],
            ["Afficher"] = ["Show", "عرض", "显示"],
            ["Période"] = ["Period", "الفترة", "时间段"],
            ["Total période"] = ["Period total", "إجمالي الفترة", "期间总量"],
            ["Moyenne annuelle"] = ["Annual average", "المتوسط السنوي", "年均值"],
            ["Oliviers récoltés"] = ["Trees harvested", "الأشجار المحصودة", "已收获树木数"],
            ["Total (kg)"] = ["Total (kg)", "الإجمالي (كغ)", "总量（千克）"],
            ["Kilogrammes récoltés par champ et par an"] = ["Kilograms harvested by field and year", "كيلوغرامات المحصول حسب الحقل والسنة", "各地块每年收获的千克数"],
            ["kg"] = ["kg", "كغ", "千克"],
            ["Une récolte par olivier et par an — suivi des productions par champ."] = ["One harvest per olive tree per year — tracking production by field.", "محصول واحد لكل شجرة زيتون سنوياً — متابعة الإنتاج حسب الحقل.", "每棵橄榄树每年收获一次 — 按地块跟踪产量。"],
            ["Extra"] = ["Extra", "ممتاز", "特级"],
            ["Vierge"] = ["Virgin", "بكر", "初榨"],
            ["Courante"] = ["Standard", "عادي", "普通"],
            ["Année de début"] = ["Start year", "سنة البداية", "开始年份"],
            ["Année de fin"] = ["End year", "سنة النهاية", "结束年份"],
            ["Langue"] = ["Language", "اللغة", "语言"],
            ["Changer"] = ["Apply", "تطبيق", "应用"],
            ["Confidentialité"] = ["Privacy", "الخصوصية", "隐私"],
            ["Les données de cette application restent stockées localement dans un fichier SQLite."]
                = ["This application's data is stored locally in an SQLite file.", "تُخزَّن بيانات هذا التطبيق محلياً في ملف SQLite.", "此应用的数据存储在本地 SQLite 文件中。"],
            ["Erreur"] = ["Error", "خطأ", "错误"],
            ["Une erreur s’est produite"] = ["An error occurred", "حدث خطأ", "发生错误"],
            ["Requête :"] = ["Request:", "الطلب:", "请求："],
            ["Champ enregistré."] = ["Field saved.", "تم حفظ الحقل.", "地块已保存。"],
            ["Champ mis à jour."] = ["Field updated.", "تم تحديث الحقل.", "地块已更新。"],
            ["Champ supprimé."] = ["Field deleted.", "تم حذف الحقل.", "地块已删除。"],
            ["Olivier enregistré."] = ["Olive tree saved.", "تم حفظ شجرة الزيتون.", "橄榄树已保存。"],
            ["Olivier mis à jour."] = ["Olive tree updated.", "تم تحديث شجرة الزيتون.", "橄榄树已更新。"],
            ["Olivier supprimé."] = ["Olive tree deleted.", "تم حذف شجرة الزيتون.", "橄榄树已删除。"],
            ["Récolte enregistrée (une récolte par olivier et par an)."] = ["Harvest saved (one harvest per olive tree per year).", "تم حفظ المحصول (محصول واحد لكل شجرة زيتون سنوياً).", "收成已保存（每棵橄榄树每年一次）。"],
            ["Récolte mise à jour."] = ["Harvest updated.", "تم تحديث المحصول.", "收成已更新。"],
            ["Récolte supprimée."] = ["Harvest deleted.", "تم حذف المحصول.", "收成已删除。"],
            ["Cet identifiant d'olivier existe déjà."] = ["This olive tree identifier already exists.", "معرّف شجرة الزيتون هذا موجود بالفعل.", "此橄榄树编号已存在。"],
            ["Cet olivier a déjà une récolte pour cette année."] = ["This olive tree already has a harvest for this year.", "لدى شجرة الزيتون هذه محصول مسجل لهذه السنة بالفعل.", "这棵橄榄树今年已有收成记录。"],
        };

    public string this[string frenchText]
    {
        get
        {
            var language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var index = language switch
            {
                "en" => 0,
                "ar" => 1,
                "zh" => 2,
                _ => -1
            };
            return index >= 0 && Translations.TryGetValue(frenchText, out var values)
                ? values[index]
                : frenchText;
        }
    }
}
