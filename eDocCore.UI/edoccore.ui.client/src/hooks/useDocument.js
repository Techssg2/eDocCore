import { useState, useEffect } from 'react';

// Custom hook m?u: l?y d? li?u t�i li?u (document)
function useDocument(documentId) {
  const [document, setDocument] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!documentId) return;
    setLoading(true);
    // Gi? l?p g?i API l?y d? li?u t�i li?u
    setTimeout(() => {
      setDocument({ id: documentId, name: `Document #${documentId}` });
      setLoading(false);
    }, 1000);
  }, [documentId]);

  return { document, loading };
}

export default useDocument;
